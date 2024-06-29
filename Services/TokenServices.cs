using Entity.Identity;
using Entity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration config , UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:key"]));
        }
        public async Task<string> CreatTokenAsync(AppUser user)
        {

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email , user.Email),
               new Claim(ClaimTypes.Name , user.UserName)
            };

           var roles = await  _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var creeds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
          
            var Token = new JwtSecurityToken
                (claims: claims,
                   issuer: _config["Token:ValidIssuer"],
                   audience: _config["Token:ValidAudiance"],
                   signingCredentials: creeds,
                   expires :DateTime.Now.AddDays( double.Parse(_config["Token:ExpirationTime"]))

                );


            return new JwtSecurityTokenHandler().WriteToken(Token);
        
        }
    }
}
