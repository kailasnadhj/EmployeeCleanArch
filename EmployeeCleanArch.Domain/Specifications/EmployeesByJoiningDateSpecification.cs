using Ardalis.Specification;
using EmployeeCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Domain.Specifications
{
    public class EmployeesByJoiningDateSpecification : Specification<Employee>
    {
        public EmployeesByJoiningDateSpecification(DateTime joiningDateFloor, DateTime joiningDateCeiling)
        {
            Query.Where(c => c.IsDeleted == false && c.DateOfJoining >= joiningDateFloor && c.DateOfJoining <= joiningDateCeiling);
        }
    }
}
