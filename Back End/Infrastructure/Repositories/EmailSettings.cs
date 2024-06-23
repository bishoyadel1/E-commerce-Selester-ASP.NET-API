using Microsoft.Extensions.Options;
using MimeKit;

using MailKit.Net.Smtp;
using MailKit.Security;
using Core.IRepositories;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class EmailSettings : IEmailSettings
    {
        private readonly MailSettings options;

        public EmailSettings(IOptions<MailSettings> options)
        {
            this.options = options.Value;
        }
        public void SendEmail(Email email)
        {
            try
            {
                var mail = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(options.Email),
                    Subject = email.Subject,
                };

                mail.To.Add(MailboxAddress.Parse(email.To));
                var builder = new BodyBuilder
                {
                    HtmlBody = email.Body
                };
                mail.Body = builder.ToMessageBody();
                mail.From.Add(new MailboxAddress(options.DisplayName, options.Email));

                using var smtp = new SmtpClient();

                // Custom SSL validation callback
                smtp.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    // Ignore certificate errors (development only)
                    return true;
                };

                smtp.Connect(options.Host, options.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(options.Email, options.Password);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
