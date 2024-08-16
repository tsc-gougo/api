namespace Domain.Options;

public class CorsConfigOptions
{
    public const string Cors = nameof(Cors);
    public IEnumerable<string>? OriginsAllowed { get; set; }
    public IEnumerable<string>? MethodsAllowed { get; set; }
}