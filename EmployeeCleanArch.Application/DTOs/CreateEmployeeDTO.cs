using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.DTOs
{
    public class CreateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime? DateOfDeparture { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;

        public long? DepartmentId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
