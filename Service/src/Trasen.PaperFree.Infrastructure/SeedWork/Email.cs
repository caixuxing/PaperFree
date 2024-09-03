using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;

namespace Trasen.PaperFree.Infrastructure.SeedWork
{
    internal class Email : IEmail
    {
        public async Task SendEmailAsync(MimeMessage message)
        {
            var emailAlarmConfig = Appsetting.Instance.GetSection("EmailAlarmConfig").Get<EmailAlarmConfig>();
            //收件人集合
            var address = new List<MailboxAddress>
            {
                new MailboxAddress("测试","315771598@qq.com")
            };
            message.From.Add(new MailboxAddress("无纸化系统邮件", emailAlarmConfig.From));
            message.To.AddRange(address);
            using var client = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.ConnectAsync(emailAlarmConfig.Host, emailAlarmConfig.Port, true);
            await client.AuthenticateAsync(emailAlarmConfig.From, emailAlarmConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}