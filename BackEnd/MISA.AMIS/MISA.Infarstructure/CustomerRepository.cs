using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Infarstructure;

namespace MISA.Infarstructure
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        #region CONSTRUCTOR
        public CustomerRepository(IConfiguration configuration):base(configuration)
        {

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
        public object GetCustomerPaging(int pageSize, int pageIndex, string customerFilter)
        {
            // Khởi tạo thông tin phân trang
            var parameter = new DynamicParameters();
            var input = customerFilter == null ? string.Empty : customerFilter;
            parameter.Add("@PageSize", pageSize, direction: ParameterDirection.Input);
            parameter.Add("@PageIndex", pageIndex, direction: ParameterDirection.Input);
            parameter.Add("@CustomerFilter", input, direction: ParameterDirection.Input);
            parameter.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Thực hiện truy vấn dữ liệu
            var customers = _dbConnection.Query<Customer>(
                "Proc_GetCustomersFilterPaging", 
                parameter, 
                commandType: CommandType.StoredProcedure);

            // Trả về dữ liệu
            // <param name="TotalPage">Tổng số trang</param>
            // <param name="TotalRecord">Tổng số bản ghi</param>
            // <param name="Data">Danh sách khách hàng</param>
            var obj = new
            {
                TotalPage = parameter.Get<int>("TotalPage"),
                TotalRecord = parameter.Get<int>("TotalRecord"),
                Data = customers
            };

            return obj;
        }

        #endregion
    }
}
