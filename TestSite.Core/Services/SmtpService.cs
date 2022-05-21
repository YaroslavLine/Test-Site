using Umbraco.Core.Logging;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using TestSite.Core.ViewModels;
using TestSite.Core.Controllers;

namespace TestSite.Core.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly ILogger _logger;

        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }

        public bool SendEmail(ContactViewModel model)
        {
            try
            {
                SmtpSection config = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                MailAddress from = new MailAddress(config.From, config.From);
                MailAddress to = new MailAddress(model.Email, model.Email);
                MailMessage message = new MailMessage(from, to);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = $"Enquiry from {model.Name}";
                message.Body += model.Message;

                SmtpClient client = new SmtpClient();
                client.EnableSsl = config.Network.EnableSsl;
                client.Host = config.Network.Host;
                client.Port = config.Network.Port;
                client.Credentials = new NetworkCredential(config.Network.UserName, config.Network.Password);
                client.Send(message);
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(ContactSurfaceController), ex, "Error sending contact form");
                return false;
            }
        }
    }
}
