using E_Commerce_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _Configuration;

        public JwtService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string GenerateSecurityToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JwtConfig:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserEmail),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _Configuration["JwtConfig:Issuer"],
                audience: _Configuration["JwtConfig:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_Configuration["JwtConfig:TokenValidityMins"]!)),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
