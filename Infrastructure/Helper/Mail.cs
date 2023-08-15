using Infrastructure.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class Mail
    {
        public static async Task SendMailAsync(FormSendMail tokenmodel)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            
            Client.EnableSsl= true;
            Client.Credentials = new NetworkCredential("bishoyadel959@gmail.com", "hxktjqnupcllmceb");
            var message = new MailMessage("bishoyadel959@gmail.com",tokenmodel.email , tokenmodel.title, tokenmodel.body);
            await Client.SendMailAsync(message);
        }
    }
}
