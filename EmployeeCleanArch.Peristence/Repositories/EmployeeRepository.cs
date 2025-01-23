using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCleanArch.Peristence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;  

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeesByDepartmentAsync(long departmentId)
        {
            var employees = await _dbContext.Employees
                .Where(e => e.DepartmentId == departmentId)  
                .ToListAsync();  

            return employees;
        }
    }
}
