using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdAsync(long id);

        Task<List<Department>> GetAllDepartmentsAsync();

        Task<Department> AddDepartmentAsync(Department department);

        Task<Department> UpdateDepartmentAsync(Department department);

        Task DeleteDepartmentAsync(long id);
    }
}
