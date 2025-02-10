using Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Interface
{
    public interface IJwtManagement
    {
        JwtResult GenerateTokens(string userName, List<Claim> claims);

        JwtResult RefreshToken(string refreshToken, string accessToken);
        void RemoveRefreshTokenByUserName(string userName);
        (ClaimsPrincipal, JwtSecurityToken?) DecodeJwtToken(string token);
    }
}