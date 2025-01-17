﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.DTOs
{
    public class CreateDepartmentDTO
    {
        public string DepartmentName { get; set; }
        public string? Location { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
