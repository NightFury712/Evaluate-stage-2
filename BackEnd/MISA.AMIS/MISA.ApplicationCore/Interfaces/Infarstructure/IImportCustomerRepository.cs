using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Interfaces.Infarstructure
{
    public interface IImportCustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Phương thức thực hiện nhập khẩu danh sách khách hàng
        /// </summary>
        /// <param name="customers"> danh sách khách hàng </param>
        /// <returns> Số cột bị ảnh hưởng </returns>
        /// Author: HHDang (17/09/2021)
        //int ImportCustomers(List<Customer> customers);

    }
}
