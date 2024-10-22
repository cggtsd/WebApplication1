using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class EmailService(IOptions<SMTPConfigModel> smtpConfig) : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig = smtpConfig.Value;

        public async Task SendTestEmail(UserEmailOptions emailOptions)
        {
            //emailOptions.Subject = "This is test email subject from book store web app";
            emailOptions.Subject = UpdatePlaceholders("Hello {{Username}},This is test email subject from book store web app", emailOptions.Placeholders);
            //emailOptions.Body = GetEmailBody("TestEmail");
            emailOptions.Body = UpdatePlaceholders(GetEmailBody("TestEmail"),emailOptions.Placeholders);
            await SendEmail(emailOptions);
            
        } 
        public async Task SendEmailForConfirmation(UserEmailOptions emailOptions)
        {
           

            emailOptions.Subject = UpdatePlaceholders("Hello {{Username}},Confirm your email id :",emailOptions.Placeholders);


            emailOptions.Body = UpdatePlaceholders(GetEmailBody("EmailConfirm"),emailOptions.Placeholders);
            await SendEmail(emailOptions);
            
        } 
        public async Task SendEmailForForgotPassword(UserEmailOptions emailOptions)
        {
           

            emailOptions.Subject = UpdatePlaceholders("Hello {{username}},reset your password :",emailOptions.Placeholders);


            emailOptions.Body = UpdatePlaceholders(GetEmailBody("ForgotPassword"),emailOptions.Placeholders);
            await SendEmail(emailOptions);
            
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mailMessage = new()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,

            };
            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mailMessage.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential

            };
            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);
        }

        private string GetEmailBody(string templateName)
        {
            //var body = File.ReadAllText(templateName);  
            string body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        private string UpdatePlaceholders(string text,List<KeyValuePair<string,string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text=text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
