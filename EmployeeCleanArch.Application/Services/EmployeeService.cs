﻿using EmployeeCleanArch.Application.Interfaces.Services;
using EmployeeCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<Department> AddEmployeeAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployeeAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetEmployeeByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Department> UpdateEmployeeAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
