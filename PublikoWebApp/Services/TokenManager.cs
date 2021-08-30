using Microsoft.AspNetCore.Identity;
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
    public static class TokenManager
    {
        private const string SECRET_KEY = "kdhfjksdhfjk89347589ueroghdfjklgh8954tyu9845hginrtgol856y7";
        public static readonly SymmetricSecurityKey SIGN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        public static string GenerateJwtToken(PublikoUser user)
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
                //{ "access", userAccess },
                { "exp", ts },
                { "iss", "https://localhost:44353/" }, //https://localhost:44353/
                { "aud", "https://localhost:5001" } //https://localhost:5001
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}
