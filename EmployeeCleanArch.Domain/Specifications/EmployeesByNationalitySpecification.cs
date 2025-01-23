using Ardalis.Specification;
using EmployeeCleanArch.Domain.Entities;

namespace EmployeeCleanArch.Domain.Specifications
{
    public class EmployeesByNationalitySpecification : Specification<Employee>
    {
        public EmployeesByNationalitySpecification(string nationality)
        {
            Query.Where(c => c.IsDeleted == false && c.Nationality == nationality);
        }
    }
}
