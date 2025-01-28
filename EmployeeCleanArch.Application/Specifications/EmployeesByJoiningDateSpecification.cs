using Ardalis.Specification;
using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Domain.Specifications
{
    public class EmployeesByJoiningDateSpecification : Specification<Employee>
    {
        public EmployeesByJoiningDateSpecification(DateTime joiningDateFloor, DateTime joiningDateCeiling)
        {
            Query.Where(c => c.IsDeleted == false && c.DateOfJoining >= joiningDateFloor && c.DateOfJoining <= joiningDateCeiling);
            //Query.Select(x => x.Adapt(new GetEmployeeDTO()));
        }
    }
}
