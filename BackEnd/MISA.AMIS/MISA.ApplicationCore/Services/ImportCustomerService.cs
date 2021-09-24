using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Const;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces.Infarstructure;
using MISA.ApplicationCore.Interfaces.Service;

namespace MISA.ApplicationCore.Services
{
    public class ImportCustomerService : BaseService<Customer>, IImportCustomerService
    {
        #region DECLARE
        IImportCustomerRepository _importCustomerRepository;
        #endregion

        #region CONSTRUCTOR
        public ImportCustomerService(IImportCustomerRepository importCustomerRepository):base(importCustomerRepository)
        {
            _importCustomerRepository = importCustomerRepository;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Thực hiện validate để nhập khẩu dữ liệu
        /// </summary>
        /// <param name="customers">Danh sách khách hàng</param>
        /// <returns>Kết quả và thông điệp</returns>
        /// Author: HHDang (17/09/2021)
        public ServiceResult Import(List<Customer> customers)
        {
            bool isExist = true;

            foreach (var customer in customers)
            {
                if(customer.CustomerId == Guid.Empty)
                {
                    customer.EntityState = EntityState.AddNew;
                    continue;
                }
                // Kiểm tra xem bản ghi có tồn tại hay chưa
                isExist = _importCustomerRepository.CheckEntityExist(customer.CustomerId);
                if(isExist)
                {
                    // Nếu đã tồn tại thì cập nhật trạng thái là sửa
                    customer.EntityState = EntityState.Update;
                } 
                else
                {
                    // Nếu chưa tồn tại thì cập nhật trạng thái là thêm mới
                    customer.EntityState = EntityState.AddNew;
                }
            }
            return base.ImportEntities(customers);
        }

        protected override bool ValidateCustom(Customer customer)
        {
            bool isValid = false;
            string province = customer.Province;
            string district = customer.District;
            string ward = customer.Ward;

            if(string.IsNullOrEmpty(province) && string.IsNullOrEmpty(district) && string.IsNullOrEmpty(ward))
            {
                isValid = true;
            }

            if(!isValid)
            {
                isValid = _importCustomerRepository.CheckAddressBelongTo("Ward", "District", ward, district);
                if(!isValid)
                {
                    var msg = new
                    {
                        devMsg = new { msg = string.Format(Properties.Resources.SR_Address_NotBelongTo, ward, district) },
                        userMsg = string.Format(Properties.Resources.SR_Address_NotExist, ward, district),
                        Code = MISAConst.NotValid
                    };
                    serviceResult.MISACode = MISACode.NotValid;
                    serviceResult.Messenger = string.Format(Properties.Resources.SR_Address_NotBelongTo, ward, district);
                    serviceResult.Data = msg;
                    return isValid;
                }
                // Kiểm tra 
                isValid = _importCustomerRepository.CheckAddressBelongTo("District", "Province", district, province);
                if (!isValid)
                {
                    var msg = new
                    {
                        devMsg = new { msg = string.Format(Properties.Resources.SR_Address_NotBelongTo, district, province) },
                        userMsg = string.Format(Properties.Resources.SR_Address_NotExist, district, province),
                        Code = MISAConst.NotValid
                    };
                    serviceResult.MISACode = MISACode.NotValid;
                    serviceResult.Messenger = string.Format(Properties.Resources.SR_Address_NotBelongTo, district, province);
                    serviceResult.Data = msg;
                    return isValid;
                }
            }
            return isValid;
        }

        #endregion

    }
}
