using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class DeleteCitizenRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteCitizenResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DeleteCitizen deleteCitizen { get; set; }
    }

    public class DeleteCitizen
    {
        public int UserId { get; set; }
    }
}
