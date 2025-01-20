using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetAllEmployees
{
    public record GetAllEmployeesQuery() : IRequest<APIResponse<IEnumerable<Employee>>>;

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, APIResponse<IEnumerable<Employee>>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetAllEmployeesQueryHandler(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<IEnumerable<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {

            var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<Employee>>.Success(employees, "Employees fetched successfully.");
            }

            return APIResponse<IEnumerable<Employee>>.Failure("No employees found.", HttpStatusCode.NotFound);
        }
    }
}
