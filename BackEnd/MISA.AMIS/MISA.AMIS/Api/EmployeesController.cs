using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.ApplicationCore.EmailServices.Interfaces;
using MISA.ApplicationCore.EmailServices.Entities;

namespace MISA.CukCuk.Web.Api
{

    [ApiController]
    public class EmployeesController : BaseApiController<Employee>
    {
        #region Declare
        private readonly IEmployeeService _employeeService;
        private readonly IEmailSender _emailSender;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IEmailSender emailSender):base(employeeService)
        {
            _employeeService = employeeService;
            _emailSender = emailSender;
        }
        #endregion

        #region Api
        /// <summary>
        /// Phương thức phân trang cho thực thể nhân viên
        /// </summary>
        /// <param name="pageSize"> Số lượng bản ghi mỗi trang </param>
        /// <param name="pageIndex"> Số Trang </param>
        /// <param name="employeeFilter"> Thông tin tìm kiếm </param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <returns> Danh sách nhân viên </returns>
        /// Author: HHDang (30/7/2021)
        [HttpGet("employeeFilter")]
        public IActionResult Filter(int pageSize, int pageIndex, string employeeFilter)
        {
            try
            {
                var message = new Message(new string[] { "haidangk63@gmail.com" }, "Test email", "This is a content of email.");
                _emailSender.SendEmail(message);

                var employees = _employeeService.GetEmployeePaging(pageSize, pageIndex, employeeFilter);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));
            }
            
        }

        /// <summary>
        /// Lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: HHDang (30/7/2021)
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                return Ok(_employeeService.GetNewEmployeeCode());

            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));
            }
        }

        /// <summary>
        /// Phương thức xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeeDeleteList">Danh sách nhân viên</param>
        /// <returns> Thông điệp </returns>
        /// Author: HHDang (11/8/2021)
        [HttpPost("deletes")]
        public IActionResult Deletes(IEnumerable<Guid> employeeDeleteList)
        {
            try
            {
                _serviceResult = _employeeService.Deletes(employeeDeleteList);
                return Ok(_serviceResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));
            }
        }
        #endregion
    }
}
