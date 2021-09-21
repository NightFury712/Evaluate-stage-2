using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Interfaces.Infarstructure
{
    /// <summary>
    /// Interfaces danh sách khách hàng
    /// </summary>
    /// Author: HHDang (16/09/2021)
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Phương thức phân trang cho thực thể khách hàng
        /// </summary>
        /// <param name="pageSize"> Số lượng bản ghi mỗi trang </param>
        /// <param name="pageIndex"> Trang hiện tại </param>
        /// <param name="customerFilter">Thông tin tìm kiếm</param>
        /// <returns> Danh sách khách hàng </returns>
        /// Author: HHDang (16/09/2021)
        object GetCustomerPaging(int pageSize, int pageIndex, string customerFilter);

    }
}
