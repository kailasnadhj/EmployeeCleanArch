namespace EmployeeCleanArch.Application.DTOs
{
    public class UpdateDepartmentDTO
    {
        public string DepartmentName { get; set; }
        public string? Location { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; }
        public string UpdatedBy { get; set; }
    }
}
