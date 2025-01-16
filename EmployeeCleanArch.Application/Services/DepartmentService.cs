using EmployeeCleanArch.Application.Interfaces;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Services
{
    public class DepartmentService() : IDepartmentService
    {
        private readonly IGenericRepository<Department> _repository;


        public Task<Department> AddDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDepartmentAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Department> GetDepartmentByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> UpdateDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
