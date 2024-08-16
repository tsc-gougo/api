using MimeKit;

namespace Application.Interfaces.Mails;

public interface IMailService
{
    (string, bool) SendMail(MimeMessage? message);
}