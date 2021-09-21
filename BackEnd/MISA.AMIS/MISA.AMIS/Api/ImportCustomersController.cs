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
    public class ImportCustomersController : BaseApiController<Customer>
    {
        #region DECLARE
        private readonly IImportCustomerService _customerService;
        #endregion

        #region CONSTRUCTOR
        public ImportCustomersController(IImportCustomerService customerService):base(customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region METHODS
        [HttpPost("reader")]
        public IActionResult Import(List<Customer> customers)
        {
            try
            {
                _serviceResult = _customerService.Import(customers);
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
