using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Infarstructure;
using MISA.ApplicationCore.Interfaces.Service;

namespace MISA.ApplicationCore.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region DECLARE
        ICustomerRepository _customerRepository;
        #endregion

        #region CONSTRUCTOR
        public CustomerService(ICustomerRepository customerRepository): base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region METHOD
        /// <summary>
        /// Phương thức phân trang cho thực thể khách hàng
        /// </summary>
        /// <param name="pageSize"> Số bản ghi mỗi trang </param>
        /// <param name="pageIndex"> Trang hiện tại </param>
        /// <param name="customerFilter">Thông tin tìm kiếm</param>
        /// <returns> Danh sách khách hàng </returns>
        /// Author: HHDang (16/09/2021)
        public object GetCustomerFilterPaging(int pageSize, int pageIndex, string customerFilter)
        {
            return _customerRepository.GetCustomerPaging(pageSize, pageIndex, customerFilter);
        }
        #endregion
    }
}
