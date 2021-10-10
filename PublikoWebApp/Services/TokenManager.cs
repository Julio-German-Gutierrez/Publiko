using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PublikoWebApp.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PublikoWebApp.Services
{
    public interface ITokenManager
    {
        Task<string> GenerateJwtToken(PublikoUser user);
    }
    public class TokenManager : ITokenManager
    {
        const string SECRET_KEY = "kdhfjksdhfjk89347589ueroghdfjklgh8954tyu9845hginrtgol856y7";
        readonly SymmetricSecurityKey SIGN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public IConfiguration _configuration { get; }
        public UserManager<PublikoUser> _userManager { get; }
        public RoleManager<PublikoUser> _roleManager { get; }

        public TokenManager(IConfiguration configuration,
                            UserManager<PublikoUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public async Task<string> GenerateJwtToken(PublikoUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id),
                    new Claim("userName", user.UserName),
                    new Claim("userEmail", user.Email),
                    new Claim("userRole", (await _userManager.GetRolesAsync(user))[0]) //Chequear esto
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(SIGN_KEY, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["APIAddresses:WebApp"],
                Audience = _configuration["APIAddresses:PublikoAPI"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
