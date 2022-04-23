using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class ReadAdminResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetAdmin> readAdmin { get; set; }
    }

    public class GetAdmin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
