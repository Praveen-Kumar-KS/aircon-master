using MailKit.Net.Smtp;
using MimeKit;
using System;
//using System.Net.Mail;
using System.Threading.Tasks;
using Aircon.Business.Helper;
using Aircon.Core;

namespace Aircon.Business.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender :  ISmsSender
    {
        private static IConfigValueProvider configSectionProvider;
        public AuthMessageSender(IConfigValueProvider _configSectionProvider)
        {
            configSectionProvider = _configSectionProvider;
        }
        public void SendEmail(InternetAddress email, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(configSectionProvider.GetValue("EmailFrom"), configSectionProvider.GetValue("EmailUsername")));
            emailMessage.To.Add(email);
            emailMessage.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            emailMessage.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(configSectionProvider.GetValue("EmailSmtpServer"), Convert.ToInt32(configSectionProvider.GetValue("EmailPort")), true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(configSectionProvider.GetValue("EmailUsername"), configSectionProvider.GetValue("EmailPassword"));

                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception or both.
                    throw new AppException("Error in send mail.");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }            
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
