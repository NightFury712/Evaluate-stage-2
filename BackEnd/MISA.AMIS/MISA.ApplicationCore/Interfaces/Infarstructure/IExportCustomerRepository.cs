using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Interfaces.Infarstructure
{
    public interface IExportCustomerRepository : IBaseRepository<Customer>
    {
        //IEnumerable<Customer> ExportCustomers(int start, int end);
    }
}
