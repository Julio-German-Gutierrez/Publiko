using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PublikoWebApp.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublikoWebApp.Services
{
    public interface ITokenManager
    {
        string GenerateJwtToken(PublikoUser user);
    }
    public class TokenManager : ITokenManager
    {
        const string SECRET_KEY = "kdhfjksdhfjk89347589ueroghdfjklgh8954tyu9845hginrtgol856y7";
        readonly SymmetricSecurityKey SIGN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public IConfiguration _configuration { get; }

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateJwtToken(PublikoUser user)
        {
            var credentials = new SigningCredentials(SIGN_KEY, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            DateTime Expire = DateTime.UtcNow.AddMinutes(1);
            int ts = (int)(Expire - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload()
            {
                { "id", user.Id },
                { "name", user.UserName },
                { "email", user.Email },
                { "exp", ts },
                { "iss", _configuration.GetSection("APIAddresses").GetSection("WebApp").Value },    //5010
                { "aud", _configuration.GetSection("APIAddresses").GetSection("PublikoAPI").Value } //5000
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}
