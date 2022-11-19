using Application.Dtos;
using Application.Interfaces;
using Application.Options;
using Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {   
        private readonly IOptions<AppJwtTokenConfigurationOptions> _tokenOption;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IOptions<AppJwtTokenConfigurationOptions> tokenOption)
        {   
            _tokenOption = tokenOption;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.Value.Key));
        }
        public string CreateToken(AppUser user, IList<string> Roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
            };
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_tokenOption.Value.ExpiryInDays),
                SigningCredentials = creds,
                Issuer = _tokenOption.Value.Issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public bool ValidateCurrentToken(string token)
        {
            
            //var myAudience = "http://myaudience.com";
            if(token.IndexOf("Bearer ") != -1)
            {
                token = token.Replace("Bearer ", "");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = _tokenOption.Value.Issuer,
                   //ValidAudience = myAudience,
                    IssuerSigningKey = _key
                }, out SecurityToken validatedToken);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

    }
}
