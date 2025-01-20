using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(long id) : IRequest<APIResponse<Employee>>;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetEmployeeByIdQueryHandler(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeData = await _repository.GetByIdAsync(request.id);
            if (employeeData != null)
            {
                return APIResponse<Employee>.Success(employeeData, "Employee fetched successfully.");
            }
            return APIResponse<Employee>.Failure("Employee with the specified ID not found.", HttpStatusCode.NotFound);

        }
    }
}
