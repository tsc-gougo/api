using Application.Interfaces.Business;
using Application.Interfaces.Mails;
using Domain.DataTransfersObject.Inputs.Contacts;
using Domain.Options;
using Infrastructure.Resources;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Businesses;

public class BusinessService : IBusinessService
{
    private readonly MailConfigOption _mailOption;
    private readonly IMailService _mailService;

    public BusinessService(IMailService mailService, IOptions<MailConfigOption> mailOption)
    {
        _mailService = mailService;
        _mailOption = mailOption.Value;
    }

    public void SendContactMail(ContactInput input, CancellationToken cancellationToken = default)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress("gougo-ai", _mailOption.From));
        mimeMessage.To.Add(new MailboxAddress("GOUGO AI", _mailOption.ContactMail));

        mimeMessage.Subject = "Nuevo Contacto Recibido";

        var message = MailTemplates.MailContact
            .Replace("[Name]", input.Name)
            .Replace("[Email]", input.Email)
            .Replace("[PhoneNumber]", input.PhoneNumber)
            .Replace("[BusinessType]", input.BusinessType)
            .Replace("[BusinessRole]", input.BusinessRole)
            .Replace("[BusinessArea]", input.BusinessArea)
            .Replace("[Country]", input.Country)
            .Replace("[HasPersonalInCountry]", input.HasPersonalInCountry ? "Si" : "No");

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message,
            TextBody = message
        };

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        _ = _mailService.SendMail(mimeMessage);
    }
}