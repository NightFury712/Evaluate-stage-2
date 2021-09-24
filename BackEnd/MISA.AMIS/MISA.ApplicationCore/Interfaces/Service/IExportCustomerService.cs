using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;

namespace MISA.ApplicationCore.Interfaces.Service
{
    public interface IExportCustomerService : IBaseService<Customer>
    {
        byte[] ExportCustomers(int start, int end);
    }
}
