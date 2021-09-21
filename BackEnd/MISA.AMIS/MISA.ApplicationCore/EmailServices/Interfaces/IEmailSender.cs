using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.EmailServices.Entities;

namespace MISA.ApplicationCore.EmailServices.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
