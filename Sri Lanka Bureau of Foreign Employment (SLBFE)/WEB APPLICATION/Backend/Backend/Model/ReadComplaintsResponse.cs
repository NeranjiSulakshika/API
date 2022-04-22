using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class ReadComplaintsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ReadComplaints> readComplaints { get; set; }
    }

    public class ReadComplaints
    {
        public int ComplaintId { get; set; }
        public string? Complaint { get; set; }
        public string Reply { get; set; }
    }
}
