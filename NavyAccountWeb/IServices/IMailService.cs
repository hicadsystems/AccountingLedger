using NavyAccountWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IMailService
    {
        void SendEmail(MailClass mail);
        string sendsms(smxmodel mail);
        string sendemail(MailClass mail);
        string SendEmailTermii(MailClass mail);
        string sendsmsTermii(smxmodel mail);
    }
}
