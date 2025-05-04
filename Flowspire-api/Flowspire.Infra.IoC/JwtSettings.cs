namespace Flowspire.Infra.IoC;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}
