using Application.Dtos;
using Application.Interfaces;
using Application.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<AppEmailOptions> _emailOptions;

        public EmailService(IOptions<AppEmailOptions> emailOptions)
        {
            _emailOptions = emailOptions;
        }
        public async Task<bool> SendMail(EmailDto email)
        {
            // if from email is not provided then take it from appSettings.json
            // Ideally it might always take from appSettings.json but kept it this
            // way just for flexibility , in case we need.
            if (string.IsNullOrWhiteSpace(email.From))
            {
                email.From = _emailOptions.Value.FromEmail;
                email.Password = _emailOptions.Value.FromEmailPassword;
            }
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
                if (!string.IsNullOrWhiteSpace(ccMail))
                {
                    message.CC.Add(new MailAddress(ccMail));
                }
                    
            }
            foreach (var bccMail in email.BCC)
            {
                if(!string.IsNullOrWhiteSpace(bccMail))
                {
                    message.Bcc.Add(new MailAddress(bccMail));
                }
                
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Port = _emailOptions.Value.Port;
            smtp.Host = _emailOptions.Value.Host; 
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
