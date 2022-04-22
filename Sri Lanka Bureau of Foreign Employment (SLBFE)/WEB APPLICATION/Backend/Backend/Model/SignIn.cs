using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Affiliation { get; set; }
    }

    public class SignInResponse
    { 
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
