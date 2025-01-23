using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesByDepartmentAsync(long departmentId);
    }
}
