using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthenticationController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }


        
        [HttpPost]
        [Route("Registration")]
        public bool Registration(DTOForSignUp dTOForSignUp)
        {
            var createUser = new User() { Username = dTOForSignUp.Username.ToLower() };
            var data=_authRepo.Registration(createUser,dTOForSignUp.Password);
            return data;
        }
        [HttpPost]
        [Route("Login")]
        public DTOAuthenticationResponse Login(DTOForLogin dTOForLogin)
        {
            DTOAuthenticationResponse dTOAuthenticationResponse = new DTOAuthenticationResponse();
            var data= _authRepo.Login(dTOForLogin.Username, dTOForLogin.Password);
            dTOAuthenticationResponse.Username = data.UserName;
            dTOAuthenticationResponse.Secret = data.SecretToken;
            return dTOAuthenticationResponse;


        }

    }
}
