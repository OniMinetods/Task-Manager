using firstPetProject.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace firstPetProject.Core.Auth.BusinessLogic
{
    public class JwtService(IOptions<AuthSettings> options)
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("id", user.UserID.ToString())
            };
            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials:
                new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
