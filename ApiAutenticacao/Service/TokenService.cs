using Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAutenticacao.Service
{
    public static class TokenService
    {
        public static string Secret = "auth1234qwer5678tyui90io";
        public static string GenerateToken(Login entrada)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);

            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Name, entrada.LoginNome));
            claim.Add(new Claim(ClaimTypes.NameIdentifier, entrada.LoginNome));
            claim.Add(new Claim("IdLogin", entrada.LoginId.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddMonths(2),
                Audience = "https://localhost:7206/",
                Issuer = "https://localhost:7174/, https://localhost:7206/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
