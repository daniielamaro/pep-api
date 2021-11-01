using Dominio.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Servicos
{
    public class TokenService
    {
        public static string GenerateToken(Paciente paciente)
        {
            string secret = Startup.Configuration.GetSection("Secret").Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, paciente.Id.ToString()),
                    new Claim(ClaimTypes.Role, "paciente")
                }),

                Expires = DateTime.UtcNow.AddDays(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            string secret = Startup.Configuration.GetSection("secret").Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try { tokenHandler.ValidateToken(token.Trim(), validationParameters, out SecurityToken securityToken); }
            catch (SecurityTokenException) { return false; }
            catch (Exception) { return false; }

            return true;
        }
    }
}
