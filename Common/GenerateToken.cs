using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Examination_System.Common
{
    public  class GenerateToken
    {
        private readonly IConfiguration _config;

        public GenerateToken(IConfiguration config)
        {
            _config = config;
        }
        public  string Generate(int userID, string Name, string Role)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "",
                Audience = "",
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("ID", userID.ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Name, Name),
                    new System.Security.Claims.Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
