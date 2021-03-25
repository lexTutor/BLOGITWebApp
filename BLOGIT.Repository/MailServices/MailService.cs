using BLOGIT.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace BLOGIT.Repository
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// Sends a mail to a given user or set of users using System.Net.Mail SmtpClient
        /// </summary>
        /// <param name="mailDetails"></param>
        public void SendMail(MailDetails mailDetails)
        {
            var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient();

            try
            {
                //Adds each email in Receivers to the MailAddressCollection
                foreach (string receiver in mailDetails.Recievers)
                {
                    mailMessage.To.Add(new MailAddress(receiver));
                }

                //Configures the sender, receiver(s), title and body of the message to be sent
                mailMessage.From = new MailAddress(_mailSettings.Mail);
                mailMessage.Subject = mailDetails.MessageTitle;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = mailDetails.MessageBody;

                smtpClient.Port = _mailSettings.Port;
                smtpClient.Host = _mailSettings.Server;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
            }
        }
    }
}
