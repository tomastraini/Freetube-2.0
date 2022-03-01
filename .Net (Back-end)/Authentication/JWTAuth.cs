using Microsoft.IdentityModel.Tokens;
using REST.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace REST.Authentication
{
    public class JWTAuth: IJWTAuth
    {
        public readonly string key;
        public JWTAuth(string key)
        {
            this.key = key;
        }
        public string Authentication(string username, string password, List<users> userCredential)
        {
            if (!(userCredential.Select(x => x.usern).Contains(username)) ||
                !(userCredential.Select(x => x.passwordu).Contains(password)))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

