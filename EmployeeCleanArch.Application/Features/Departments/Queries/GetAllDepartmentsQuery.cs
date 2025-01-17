using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;

namespace EmployeeCleanArch.Application.Features.Departments.Queries
{
    public record GetAllDepartmentsQuery() : IRequest<IEnumerable<Department>>;

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<Department>>
    {
        private readonly IGenericRepository<Department> _repository;

        public GetAllDepartmentsQueryHandler(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
