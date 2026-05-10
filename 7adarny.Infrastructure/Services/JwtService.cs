using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Domin.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(int userId, string role, string name)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Expires = DateTime.Now.AddMinutes(int.Parse(_config["Jwt:ExpirationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim (ClaimTypes.Name,name),
                    new Claim (ClaimTypes.NameIdentifier,userId.ToString()),
                    new Claim (ClaimTypes.Role,role)
                })
            };
            //of type security token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return token as string
            return tokenHandler.WriteToken(token);
        }
    }
}
