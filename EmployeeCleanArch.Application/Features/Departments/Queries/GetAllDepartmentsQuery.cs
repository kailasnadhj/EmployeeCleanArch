using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Queries
{
    public record GetAllDepartmentsQuery() : IRequest<APIResponse<IEnumerable<Department>>>;

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, APIResponse<IEnumerable<Department>>>
    {
        private readonly IGenericRepository<Department> _repository;

        public GetAllDepartmentsQueryHandler(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<IEnumerable<Department>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {

                var departments = await _repository.GetAllAsync();

                if (departments != null && departments.Any())
                {
                    return APIResponse<IEnumerable<Department>>.Success(departments, "Departments fetched successfully.");
                }

                return APIResponse<IEnumerable<Department>>.Failure("No departments found.", HttpStatusCode.NotFound);
        }
    }
}
