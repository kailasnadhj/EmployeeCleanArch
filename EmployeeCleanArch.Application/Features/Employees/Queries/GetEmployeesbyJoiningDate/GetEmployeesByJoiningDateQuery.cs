using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyJoiningDate
{
    public record GetEmployeesByJoiningDateQuery(DateTime joiningDateFloor,DateTime joiningDateCeiling) : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>;

    public class GetEmployeesByJoiningDateQueryHandler : IRequestHandler<GetEmployeesByJoiningDateQuery, APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeesByJoiningDateQuery> _validator;

        public GetEmployeesByJoiningDateQueryHandler(IGenericRepository<Employee> repository, IValidator<GetEmployeesByJoiningDateQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<IEnumerable<GetEmployeeDTO>>> Handle(GetEmployeesByJoiningDateQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var spec = new EmployeesByJoiningDateSpecification(request.joiningDateFloor,request.joiningDateCeiling);
            var employeesList = await _repository.GetAllAsync(spec, cancellationToken);
            var employees = employeesList.Adapt<IEnumerable<GetEmployeeDTO>>();


            //var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<GetEmployeeDTO>>.Success(employees, "Employees within the filter range fetched successfully.");
            }

            return APIResponse<IEnumerable<GetEmployeeDTO>>.Failure("No employees found for the given filter.", HttpStatusCode.NotFound);
        }
    }
}
