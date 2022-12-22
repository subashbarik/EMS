using Application.Dtos;
using Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendMail(EmailDto email)
        {
            MailMessage message = new MailMessage();
            message.Body = email.Body;
            message.From = new MailAddress(email.From);
            message.Subject = email.Subject;
            message.IsBodyHtml = true;

            foreach(var toEmail in email.To)
            {
                message.To.Add(new MailAddress(toEmail));
            }
            foreach (var ccMail in email.CC)
            {
                message.CC.Add(new MailAddress(ccMail));
            }
            foreach (var bccMail in email.BCC)
            {
                message.Bcc.Add(new MailAddress(bccMail));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //IMPORTANT : for Password we must generate a APP password for Gmail for the windows
            // computer (in the gmail app in security tab) from where we are sending and use that password
            smtp.Credentials = new NetworkCredential(email.From, email.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
            return true;
        }
    }
}
