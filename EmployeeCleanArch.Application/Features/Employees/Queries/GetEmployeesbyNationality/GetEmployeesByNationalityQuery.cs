using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyNationality
{
    public record GetEmployeesByNationalityQuery(string nationality) : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>;

    public class GetEmployeesByNationalityQueryHandler : IRequestHandler<GetEmployeesByNationalityQuery, APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeesByNationalityQuery> _validator;

        public GetEmployeesByNationalityQueryHandler(IGenericRepository<Employee> repository, IValidator<GetEmployeesByNationalityQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<IEnumerable<GetEmployeeDTO>>> Handle(GetEmployeesByNationalityQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var spec = new EmployeesByNationalitySpecification(request.nationality);
            var employeesList = await _repository.GetAllAsync(spec, cancellationToken);
            var employees = employeesList.Adapt<IEnumerable<GetEmployeeDTO>>();


            //var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<GetEmployeeDTO>>.Success(employees, "Employees with the nationality fetched successfully.");
            }

            return APIResponse<IEnumerable<GetEmployeeDTO>>.Failure("No employees found for the entered nationality.", HttpStatusCode.NotFound);
        }
    }
}
