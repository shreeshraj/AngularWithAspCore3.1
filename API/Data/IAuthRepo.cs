using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
   public interface IAuthRepo
    {
        bool Registration(User signUpModel,string password);
        AuthenticationResponse Login(string user, string Password);
    }
}
