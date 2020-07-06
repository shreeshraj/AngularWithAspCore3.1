using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace API.Data
{
    public class AutheRepo : IAuthRepo
    {
        private readonly DatingDbContext _datingDbContext;
        private IConfiguration _config;
        public AutheRepo(DatingDbContext datingDbContext, IConfiguration config)
        {
              _config = config;
              _datingDbContext = datingDbContext;
        }

       

        public  bool Registration(User signUpModel,string password)
        {
            byte[] passwordHash, passwordSalt;
            CreateHashAndSalt(password, out passwordHash, out passwordSalt);
            signUpModel.PasswordHash = passwordHash;
            signUpModel.PasswordSalt = passwordSalt;
           _datingDbContext.users.Add(signUpModel);
           _datingDbContext.SaveChangesAsync();
            if (signUpModel.Id > 0) return true;
            else return false;
        }


        

        private void CreateHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
                
        }

        public AuthenticationResponse Login(string user, string Password)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            var loginUser=  _datingDbContext.users.FirstOrDefault(x=>x.Username== user);
            if (loginUser != null)
            {
                if (ValidateHashCode(Password, loginUser.PasswordHash, loginUser.PasswordSalt))
                {
                    
                    string secretToken=generateJwtToken(loginUser);
                    authenticationResponse.UserName = loginUser.Username;
                    authenticationResponse.SecretToken = secretToken;



                }
                
            }
            return authenticationResponse;


        }

        public bool ValidateHashCode(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
              byte[] hashpassword=  hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
                for(int i=0; i< hashpassword.Length; i = i + 1)
                {
                    if (hashpassword[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings").GetSection("secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}
