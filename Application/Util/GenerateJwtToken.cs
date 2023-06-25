using Application.ViewModel;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Util
{
    public static class GenerateJwtToken
    {
        public static string GenerateJsonWebToken(this User user,string secreteKey,DateTime now)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreteKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.RoleName),
            };
           

            var token = new JwtSecurityToken(
                issuer: secreteKey,
                audience:secreteKey,
                claims,
                expires:now.AddMinutes(120),
                signingCredentials:credentials
                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static ClaimsPrincipal? GetPrincipalFromExpiredToken(this string? token, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            else return null;


        }

        public static JwtDTO VerifyToken(this string? token, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {

                var principal = tokenHandler.ReadJwtToken(token).Claims;

                var result = new JwtDTO
                {
                    UserId = principal.First(x => x.Type == "UserId").Value,
                   RoleName=principal.First(x=>x.Type=="RoleName").Value
                };
                return result;
            }
            else return null;
        }

    }
}

