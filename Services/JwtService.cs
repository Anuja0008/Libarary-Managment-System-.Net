using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YourProject.Models;

namespace YourProject.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName), // Fix here
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = _config["JwtSettings:SecretKey"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var expiryMinutes = _config["JwtSettings:ExpiryMinutes"];

            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) ||
                string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expiryMinutes))
            {
                throw new InvalidOperationException("JWT settings are missing in appsettings.json");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(expiryMinutes)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
