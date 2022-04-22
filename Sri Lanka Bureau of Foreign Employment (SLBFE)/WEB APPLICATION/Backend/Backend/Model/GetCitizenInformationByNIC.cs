using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class CitizenInformationByNICRequest
    {
        public string? NIC { get; set; }
    }

    public class CitizenInformationByNICResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public GetCitizenInformationByNIC getCitizenInformationByNIC { get; set; }
    }

    public class GetCitizenInformationByNIC
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? Profession { get; set; }
        public string? Email { get; set; }
    }
}
