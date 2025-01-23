using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyNationality
{
    public record GetEmployeesByNationalityQuery(string nationality) : IRequest<APIResponse<IEnumerable<Employee>>>;

    public class GetEmployeesByNationalityQueryHandler : IRequestHandler<GetEmployeesByNationalityQuery, APIResponse<IEnumerable<Employee>>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeesByNationalityQuery> _validator;

        public GetEmployeesByNationalityQueryHandler(IGenericRepository<Employee> repository, IValidator<GetEmployeesByNationalityQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<IEnumerable<Employee>>> Handle(GetEmployeesByNationalityQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var spec = new EmployeesByNationalitySpecification(request.nationality);
            var employees = await _repository.GetAllAsync(spec, cancellationToken);


            //var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<Employee>>.Success(employees, "Employees with the nationality fetched successfully.");
            }

            return APIResponse<IEnumerable<Employee>>.Failure("No employees found for the entered nationality.", HttpStatusCode.NotFound);
        }
    }
}
