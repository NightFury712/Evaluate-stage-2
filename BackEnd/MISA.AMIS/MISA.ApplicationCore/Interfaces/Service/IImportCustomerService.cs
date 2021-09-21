using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Interfaces.Service
{
    public interface IImportCustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Thực hiện validate để nhập khẩu danh sách khách hàng
        /// </summary>
        /// <param name="customers">danh sách khách hàng</param>
        /// <returns>Kết quả và thông điệp</returns>
        /// Author: HHDang (17/09/2021)
        ServiceResult Import(List<Customer> customers);
    }
}
