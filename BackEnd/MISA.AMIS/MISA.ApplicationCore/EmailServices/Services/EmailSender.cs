using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MISA.ApplicationCore.EmailServices.Entities;
using MISA.ApplicationCore.EmailServices.Interfaces;

namespace MISA.ApplicationCore.EmailServices.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateMailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateMailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2>{0}</h2>", message.Content) };

            if(message.Attachments != null && message.Attachments.Any())
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "DSKhachHang.xlsx";

                bodyBuilder.Attachments.Add(fileName, message.Attachments, ContentType.Parse(contentType));
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
