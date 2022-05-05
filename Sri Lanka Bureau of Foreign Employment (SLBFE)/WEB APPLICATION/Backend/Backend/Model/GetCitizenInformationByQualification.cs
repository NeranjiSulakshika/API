using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class CitizenInformationByQualificationRequest
    {
        public string? Qualification { get; set; }
    }

    public class CitizenInformationByQualificationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetCitizenInformationByQualification> getCitizenInformationByQualification { get; set; }
    }

    public class GetCitizenInformationByQualification
    {
        public string? NIC { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? Profession { get; set; }
        public string? Email { get; set; }
    }
}
