using MimeKit;

namespace Trasen.PaperFree.Domain.SeedWork;

public interface IEmail
{
    /// <summary>
    /// 发送Email
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendEmailAsync(MimeMessage message);
}