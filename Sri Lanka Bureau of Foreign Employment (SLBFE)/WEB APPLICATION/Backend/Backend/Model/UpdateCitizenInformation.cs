﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class UpdateCitizenInformationRequest
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    public class UpdateCitizenInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
