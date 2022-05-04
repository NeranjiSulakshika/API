using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class CreateComplaintRequest
    {
        public string Complaint { get; set; }
    }

    public class CreateComplaintResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
