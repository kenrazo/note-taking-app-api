using Microsoft.IdentityModel.Tokens;
using NoteTakingApi.Common.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Services
{
    public class TokenHelper
    {
        private readonly int _expiresIn;
        private readonly AppSettings _appSettings;

        public TokenHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _expiresIn = _appSettings.JwtSettings.Expires;
        }

        public async Task<string> GenerateToken(int userId, int expireHours)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //
                    new Claim("userId", Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddHours(expireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
