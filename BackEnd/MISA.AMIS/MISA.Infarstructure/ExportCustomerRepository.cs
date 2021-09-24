using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Infarstructure;

namespace MISA.Infarstructure
{
    public class ExportCustomerRepository : BaseRepository<Customer>, IExportCustomerRepository
    {
        #region CONSTRUCTOR
        public ExportCustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion
    }
}
