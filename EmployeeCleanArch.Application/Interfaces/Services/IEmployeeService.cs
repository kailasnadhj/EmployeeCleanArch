using EmployeeCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
