using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Services.Utils;

internal static class JWTGenerator
{
    internal static JwtSecurityToken GenerateToken(JWTOptions options, List<Claim> claims, DateTime expiryTime)
    {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims.ToArray(),
                expires: expiryTime,
                signingCredentials: credentials);

            return token;
        }
}