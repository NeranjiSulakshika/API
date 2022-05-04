using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class SignUpRequest
    {
        //NIC, Name, Address, Age, Profession, Email, PassWord, Affiliation
        public string NIC { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfigPassword { get; set; }
        public string Affiliation { get; set; }
        public string Qualification { get; set; }
    }

    public class SignUpResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
