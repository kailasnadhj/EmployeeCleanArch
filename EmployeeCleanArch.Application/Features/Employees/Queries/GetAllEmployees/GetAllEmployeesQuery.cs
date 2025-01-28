using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using Mapster;
using System.Net;
using EmployeeCleanArch.Domain.Specifications;
using EmployeeCleanArch.Application.DTOs;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetAllEmployees
{
    // Using record instead of class for implementation
    //public record GetAllEmployeesQuery() : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>;
    public class GetAllEmployeesQuery : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        public GetAllEmployeesQuery() { }
    }


    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetAllEmployeesQueryHandler(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<APIResponse<IEnumerable<GetEmployeeDTO>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var spec = new IsDeletedSpecification<Employee>();
            var employeesList = await _repository.GetAllAsync(spec, cancellationToken);
            var employees = employeesList.Adapt<IEnumerable<GetEmployeeDTO>>();

            //var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<GetEmployeeDTO>>.Success(employees, "Employees fetched successfully.");
            }

            return APIResponse<IEnumerable<GetEmployeeDTO>>.Failure("No employees found.", HttpStatusCode.NotFound);
        }
    }
}
