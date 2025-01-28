using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Queries.GetAllDepartments
{
    //public record GetAllDepartmentsQuery() : IRequest<APIResponse<IEnumerable<Department>>>;
    public class GetAllDepartmentsQuery : IRequest<APIResponse<IEnumerable<Department>>>
    {
        public GetAllDepartmentsQuery() { }
    }

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, APIResponse<IEnumerable<Department>>>
    {
        private readonly IGenericRepository<Department> _repository;

        public GetAllDepartmentsQueryHandler(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<IEnumerable<Department>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {

            var spec = new IsDeletedSpecification<Department>();

            var departments = await _repository.GetAllAsync(spec, cancellationToken);

            if (departments != null && departments.Any())
            {
                return APIResponse<IEnumerable<Department>>.Success(departments, "Departments fetched successfully.");
            }

            return APIResponse<IEnumerable<Department>>.Failure("No departments found.", HttpStatusCode.NotFound);
        }
    }
}
