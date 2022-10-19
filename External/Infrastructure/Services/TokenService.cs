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
        private readonly IOptions<AppTokenConfigurationOptions> _tokenOption;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IOptions<AppTokenConfigurationOptions> tokenOption)
        {   
            _tokenOption = tokenOption;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.Value.Key));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(Convert.ToInt32(_tokenOption.Value.ExpiryInDays)),
                SigningCredentials = creds,
                Issuer = _tokenOption.Value.Issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
