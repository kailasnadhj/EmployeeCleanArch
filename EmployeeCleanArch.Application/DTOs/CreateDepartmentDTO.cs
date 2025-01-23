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
