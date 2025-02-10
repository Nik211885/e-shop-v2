namespace Infrastructure.Configuration
{
    public record IdentityAuthentication(string Authority,
        string Secret,
        string Issuer,
        string Audience,
        int AccessTokenExpiration,
        int RefreshTokenExpiration
        );
}