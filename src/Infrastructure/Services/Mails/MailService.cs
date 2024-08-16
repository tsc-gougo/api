using Application.Interfaces.Mails;
using Domain.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Mails;

public class MailService : IMailService
{
    private readonly ILogger<MailService> _logger;
    private readonly MailConfigOption _mailOptions;

    public MailService(IOptions<MailConfigOption> options, ILogger<MailService> logger)
    {
        _mailOptions = options.Value;
        _logger = logger;
    }

    public (string, bool) SendMail(MimeMessage? message)
    {
        if (message == null)
            return ("Message construction is null", false);

        var to = message.GetRecipients().FirstOrDefault();

        try
        {
            _logger.LogInformation("Host {0} - Port {1} - User {2} - StartTls {3} - Ssl {4}", _mailOptions.Host,
                _mailOptions.Port,
                _mailOptions.Username, _mailOptions.UseStartTls, _mailOptions.UseSsl);

            using var client = new SmtpClient();
            // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            if (_mailOptions.UseStartTls)
                client.Connect(_mailOptions.Host, _mailOptions.Port, SecureSocketOptions.StartTls);
            else if (_mailOptions.UseSsl)
                client.Connect(_mailOptions.Host, _mailOptions.Port);
            else
                client.Connect(_mailOptions.Host, _mailOptions.Port, SecureSocketOptions.None);

            if (!string.IsNullOrWhiteSpace(_mailOptions.Username))
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_mailOptions.Username, _mailOptions.Password);

            _logger.LogInformation($"Sending mail for {to?.Address}");
            client.Send(message);
            client.Disconnect(true);

            return ("Ok", true);
        }
        catch (Exception ex)
        {
            var msg = $"Error send mail ex:{ex?.Message ?? ex?.InnerException?.Message}, email: {to?.Address}";
            _logger.LogError(ex, msg);
            return (msg, false);
        }
    }
}