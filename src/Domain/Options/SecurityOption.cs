namespace Domain.Options;

public class SecurityOption
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Key { get; set; }
    public int ExpirationInMinutes { get; set; }
    public int RefreshTokenExpiresInMinutes { get; set; }
}