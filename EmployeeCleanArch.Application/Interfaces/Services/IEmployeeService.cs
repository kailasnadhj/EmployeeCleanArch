using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Department> GetEmployeeByIdAsync(long id);

        Task<List<Department>> GetAllEmployeesAsync();

        Task<Department> AddEmployeeAsync(Department department);

        Task<Department> UpdateEmployeeAsync(Department department);

        Task DeleteEmployeeAsync(long id);

        Task<List<Employee>> GetEmployeesByDepartmentAsync(int departmentId);

    }
}
