using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Users
{
    public class UserTokens: IUserTokens
    {
        private readonly ILogger<UserTokens> logger;
        private readonly IConfiguration config;
        public UserTokens(ILogger<UserTokens> logger,
            IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }
        public const string AuthSchemes =
            //CookieAuthenticationDefaults.AuthenticationScheme + "," +
            JwtBearerDefaults.AuthenticationScheme;
        
        public string GenerateToken(ApplicationUser user)
        {
            return new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(this.config["Jwt:Issuer"],
                    this.config["Jwt:Audience"],
                    claims: new []
                    {
                        new Claim(ClaimTypes.Email, user.Email)
                    },
                    expires: DateTime.Now.AddSeconds(30),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256)));
        }
    }
}
