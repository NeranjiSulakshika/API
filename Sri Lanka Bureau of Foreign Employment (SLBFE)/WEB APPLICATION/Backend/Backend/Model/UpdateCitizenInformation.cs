using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class UpdateCitizenInformationRequest
    {
        public string NIC { get; set; }
        public string Qualification { get; set; }
    }

    public class UpdateCitizenInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
