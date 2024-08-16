using Domain.DataTransfersObject.Inputs.Contacts;

namespace Application.Interfaces.Business;

public interface IBusinessService
{
    void SendContactMail(ContactInput input, CancellationToken cancellationToken = default);
}