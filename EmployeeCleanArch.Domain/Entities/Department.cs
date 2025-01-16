using EmployeeCleanArch.Domain.Common;

namespace EmployeeCleanArch.Domain.Entities
{
    public class Department : BaseAuditableEntity<long>
    {
        public string DepartmentName { get; set; }
        public string? Location { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Employee>? Employees { get; set; }
    }
}
