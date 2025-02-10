namespace Application.Models
{
    public record JwtResult(string AccessToken,
        string RefreshToken, 
        int AccessTokenExpiration, 
        int RefreshTokenExpiration);
}