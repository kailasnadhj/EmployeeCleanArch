using Ardalis.Specification;
using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Domain.Specifications
{
    public class EmployeesByNameSearchSpecification : Specification<Employee>
    {
        public EmployeesByNameSearchSpecification(string searchKey)
        {
            Query.Where(c => c.FirstName.Contains(searchKey) || c.LastName.Contains(searchKey));
            //Query.Select(x => x.Adapt(new GetEmployeeDTO()));
        }
    }
}
