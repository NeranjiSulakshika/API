using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class ReadCitizenInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ReadCitizenInformation> readCitizenInformation { get; set; }
    }

    public class ReadCitizenInformation
    {
        public int UserId { get; set; }
        public string? NIC { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? Profession { get; set; }
        public string? Email { get; set; }
        public string? Affiliation { get; set; }
    }
}
