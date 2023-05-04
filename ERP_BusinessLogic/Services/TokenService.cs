using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ERP_Domians.IServices;
using ERP_Domians.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ERP_BusinessLogic.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;


        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

       

        public string CreateToken(ApplicationUser applicationUser)
        {
        
            var signingCredentials = GetSigningCredentials();
            var claims =  GetClaimsAsync(applicationUser);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims.Result);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        //gets the secret key and encodes it with the associated algorithm used.
        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["Jwt:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }



        //gets the user claims that will be shown in the JWT decoded token to identify him 
        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
            };


           var roles =  await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }


        //generate token options including expiry date, issuer and audience, List of claims to be used and the signing credentials
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(

                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(Convert.ToDouble((_configuration.GetSection("Jwt")).GetSection("Lifetime").Value))
                );

            return tokenOptions;
        }


    }
}
