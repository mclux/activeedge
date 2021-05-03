using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ActiveEdge.API.Providers
{
    public static class JWTokenProvider
    {
        public static string CreateToken(UserIdentityData userIdentity, string role)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(1);
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userIdentity.UserId.ToString()),
                new Claim(ClaimTypes.Role,userIdentity.UserRole),
                new Claim("Username",userIdentity.Username),
                new Claim("FullName",userIdentity.FullName)
            });

            string sec = ConfigurationManager.AppSettings["JWT_SECURITY_KEY"];
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: ConfigurationManager.AppSettings["JWT_ISSUER"], audience: ConfigurationManager.AppSettings["JWT_AUDIENCE"],
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

    public class UserIdentityData
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }
        public string FullName { get; set; }
    }
}