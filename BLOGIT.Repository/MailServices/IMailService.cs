using BLOGIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGIT.Repository
{
    public interface IMailService
    {
        void SendMail(MailDetails mailDetails);
    }
}
