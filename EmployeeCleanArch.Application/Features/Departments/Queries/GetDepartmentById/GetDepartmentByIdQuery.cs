using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Queries.GetDepartmentById
{
    public record GetDepartmentByIdQuery(long id) : IRequest<APIResponse<Department>>;

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;

        public GetDepartmentByIdQueryHandler(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<Department>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var departmentData = await _repository.GetByIdAsync(request.id);
            if (departmentData != null)
            {
                return APIResponse<Department>.Success(departmentData, "Department fetched successfully.");
            }
            return APIResponse<Department>.Failure("Department of the specified ID found.", HttpStatusCode.NotFound);

        }
    }
}
