using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeByDepartment
{
    public record GetEmployeeByDepartmentQuery(long departmentId) : IRequest<APIResponse<IEnumerable<Employee>>>;

    public class GetEmployeeByDepartmentQueryHandler : IRequestHandler<GetEmployeeByDepartmentQuery, APIResponse<IEnumerable<Employee>>>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeeByDepartmentQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<IEnumerable<Employee>>> Handle(GetEmployeeByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var employeeData = await _repository.GetEmployeesByDepartmentAsync(request.departmentId);
            if (employeeData != null)
            {
                return APIResponse<IEnumerable<Employee>>.Success(employeeData, "Employee fetched successfully.");
            }
            return APIResponse<IEnumerable<Employee>>.Failure("Employee with the specified Department ID not found.", HttpStatusCode.NotFound);

        }
    }
}
