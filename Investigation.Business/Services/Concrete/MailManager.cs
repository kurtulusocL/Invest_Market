using Investigation.Business.Services.Abstract;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Investigation.Business.Services.Concrete
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;       

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string to, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderPassword = _configuration["EmailSettings:SenderPassword"];
                var senderName = _configuration["EmailSettings:SenderName"] ?? "investartup.com";

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail));
                mimeMessage.To.Add(new MailboxAddress(to, to));
                mimeMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body, 
                    TextBody = body 
                };
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                using var client = new SmtpClient();
                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(senderEmail, senderPassword);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);                
            }
            catch (Exception ex)
            {
                throw new Exception($"Email gönderilemedi: To={to}, Subject={subject}", ex);
            }
        }
    }
}
