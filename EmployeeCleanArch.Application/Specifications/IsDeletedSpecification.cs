using Ardalis.Specification;
using EmployeeCleanArch.Domain.Common.Interfaces;

namespace EmployeeCleanArch.Domain.Specifications
{
    public class IsDeletedSpecification<T> : Specification<T> where T : class, IHasIsDeleted
    {
        public IsDeletedSpecification()
        {
            Query.Where(c => c.IsDeleted == false);
        }
    }
}
