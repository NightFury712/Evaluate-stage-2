using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.EmailServices.Entities;
using MISA.ApplicationCore.EmailServices.Interfaces;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.CukCuk.Web.Api;

namespace MISA.AMIS.Api
{
    [ApiController]
    public class ExportCustomersController : BaseApiController<Customer>
    {
        #region DECLARE
        private readonly IExportCustomerService _exportCustomerService;
        private readonly IEmailSender _emailSender;
        #endregion

        #region CONSTRUCTOR
        public ExportCustomersController(IExportCustomerService exportCustomerService, IEmailSender emailSender) : base(exportCustomerService)
        {
            _exportCustomerService = exportCustomerService;
            _emailSender = emailSender;
        }
        #endregion

        #region METHODS
        [HttpGet("sender")]

        public IActionResult SendEmail(int start, int end)
        {
            try
            {
                var fileBytes = _exportCustomerService.ExportCustomers(start, end);
                //file.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var message = new Message(new string[] { "haidangk63@gmail.com" }, "Xuất khẩu dữ liệu khách hàng", "Dưới đây là file xuất khẩu:", fileBytes);
                _emailSender.SendEmail(message);
                _serviceResult.MISACode = MISACode.Ok;
                _serviceResult.Messenger = MISA.ApplicationCore.Properties.Resources.SR_Success_Export;
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
