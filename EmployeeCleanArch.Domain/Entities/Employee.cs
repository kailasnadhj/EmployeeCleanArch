using EmployeeCleanArch.Domain.Common;

namespace EmployeeCleanArch.Domain.Entities
{
    public class Employee : BaseAuditableEntity<long>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public string Designation {  get; set; }
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
        public Department Department { get; set; }

    }
}
