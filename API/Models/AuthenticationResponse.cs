using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string SecretToken { get; set; }
    }
}
