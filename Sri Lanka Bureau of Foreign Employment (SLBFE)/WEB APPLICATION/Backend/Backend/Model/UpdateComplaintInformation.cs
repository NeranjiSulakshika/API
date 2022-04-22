using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class UpdateComplaintInformationRequest
    {
        public int ComplaintId { get; set; }
        public string? Complaint { get; set; }
        public string Reply { get; set; }
    }

    public class UpdateComplaintInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
