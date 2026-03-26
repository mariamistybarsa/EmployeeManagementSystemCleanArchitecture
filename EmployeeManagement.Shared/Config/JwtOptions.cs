namespace EmployeeManagement.Shared.Config;

public record JwtOptions
{
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Key { get; init; } = default!;
    public int AccessTokenMinutes { get; init; } = 60;
}