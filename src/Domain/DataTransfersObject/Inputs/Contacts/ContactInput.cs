namespace Domain.DataTransfersObject.Inputs.Contacts;

public class ContactInput
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BusinessType { get; set; }
    public string? BusinessRole { get; set; }
    public string? BusinessArea { get; set; }
    public string? Country { get; set; }
    public bool HasPersonalInCountry { get; set; }
}