using Ardalis.Specification;

namespace EmployeeCleanArch.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);
        //Task<List<TResult>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
