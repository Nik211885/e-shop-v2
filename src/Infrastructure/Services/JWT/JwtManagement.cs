using Application.Helper;
using Application.Interface;
using Application.Models;
using Core.Exceptions;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.JWT
{
    public class JwtManagement : IJwtManagement
    {
        private readonly IdentityAuthentication _identityAuthentication;
        private readonly IMemoryCache _memoryCache;
        private readonly byte[] _secret;

        public JwtManagement(IOptions<IdentityAuthentication> identityAuthentication, IMemoryCacheManager memoryCacheManager)
        {
            _identityAuthentication = identityAuthentication.Value;
            _memoryCache = memoryCacheManager.GetMemoryCache(MemoryType.Security);
            _secret = Encoding.ASCII.GetBytes(_identityAuthentication.Secret);
        }
        public JwtResult GenerateTokens(string userName, List<Claim> claims)
        {
            var jwtToken = new JwtSecurityToken(
                issuer: _identityAuthentication.Issuer,
                claims: claims,
                audience: _identityAuthentication.Audience,
                expires: DateTime.UtcNow.AddMinutes(_identityAuthentication.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256)
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = GenerateRefreshToken();
            _memoryCache.Set(userName, refreshToken, DateTime.Now.AddMinutes(_identityAuthentication.RefreshTokenExpiration));
            return new JwtResult(accessToken, refreshToken,
                _identityAuthentication.AccessTokenExpiration, _identityAuthentication.RefreshTokenExpiration);
        }

        private string GenerateRefreshToken()
            => StringHelper.GeneratorRandomStringBase64(32);

        public JwtResult RefreshToken(string refreshToken, string accessToken)
        {
            // check refresh token is pass signal
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken is null || jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
            {
                throw new UnauthorizedException("Invalid JWT token");
            }
            var userName = principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value
                           ?? throw new UnauthorizedException("user name");
            // want check access still express
            if (!_memoryCache.TryGetValue(userName, out var refresh))
            {
                throw new UnauthorizedException("Invalid refresh token");
            }
            if (refresh is null || !refresh.Equals(refreshToken))
            {
                throw new UnauthorizedException("Invalid refresh token");
            }
            return GenerateTokens(userName, principal.Claims.ToList());
        }

        public void RemoveRefreshTokenByUserName(string userName)
            => _memoryCache.Remove(userName);

        public (ClaimsPrincipal, JwtSecurityToken?) DecodeJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Invalid token");
            }
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _identityAuthentication.Issuer,
                ValidateAudience = true,
                ValidAudience = _identityAuthentication.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secret),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }
    }
}