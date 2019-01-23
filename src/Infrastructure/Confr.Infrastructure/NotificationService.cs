using System;
using System.Net;
using System.Net.Mail;
using Confr.Application.Interfaces;
using Confr.Application.Notifications.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Confr.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendAsync(Message message)
        {
            var fromAddress = new MailAddress(
                _configuration["EmailSettings:Email"],
                _configuration["EmailSettings:Name"]);

            var toAddress = GetToAddress(message);

            var smtp = GetSmtpClientConfig(fromAddress);

            using (var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = message.Subject,
                Body = message.Body
            })
            {
                smtp.Send(mailMessage);
            }

            return Task.CompletedTask;
        }

        private MailAddress GetToAddress(Message message)
        {
            if (message.To == null)
            {
                return new MailAddress(
                    _configuration["EmailSettings:Email"],
                    _configuration["EmailSettings:Name"]);
            }
            else
            {
                return new MailAddress(message.To);
            }
        }

        private SmtpClient GetSmtpClientConfig(MailAddress fromAddress)
        {
            return new SmtpClient
            {
                Host = _configuration["EmailSettings:Host"],
                Port = Convert.ToInt32(_configuration["EmailSettings:Port"]),
                EnableSsl = Convert.ToBoolean(_configuration["EmailSettings:EnableSSL"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _configuration["EmailSettings:Password"])
            };
        }
    }
}
