using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.CukCuk.Web.Api;

namespace MISA.AMIS.Api
{
    [ApiController]
    public class CustomersController : BaseApiController<Customer>
    {
        #region DECLARE
        ICustomerService _customerService;
        #endregion

        #region CONSTRUCTOR
        public CustomersController(ICustomerService customerService):base(customerService)
        {
            _customerService = customerService;
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
        [HttpGet("customerFilter")]
        public IActionResult Filter(int pageSize, int pageIndex, string customerFilter)
        {
            try
            {
                var customers = _customerService.GetCustomerFilterPaging(pageSize, pageIndex, customerFilter);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));
            }
        }
        #endregion
    }
}
