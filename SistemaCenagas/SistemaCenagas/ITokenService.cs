using Microsoft.IdentityModel.Tokens;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, Usuarios user, ApplicationDbContext context);
        bool IsTokenValid(string key, string issuer, string token);
    }

    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 180;

        public string BuildToken(string key, string issuer, Usuarios user, ApplicationDbContext context)
        {
            /*
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);


            return new JwtSecurityTokenHandler().WriteToken(createdToken);
            */

            var nombreCompleto = context.Usuarios.Where(u => u.Id == user.Id).Select(u => $"{u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            var rol = context.Roles.Where(r => r.Id == user.Id_Rol).Select(r => r.Nombre).FirstOrDefault();

            var claims = new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim("Id_Rol", user.Id_Rol.ToString()),
                new Claim("Titulo", user.Titulo == null ? "" : user.Titulo),
                new Claim("Nombre", user.Nombre == null ? "" : user.Nombre),
                new Claim("Email", user.Email == null ? "" : user.Email),
                new Claim("Rol", rol),
                new Claim("NombreCompleto", nombreCompleto),
                
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddDays(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = issuer,
                    //ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }


}
