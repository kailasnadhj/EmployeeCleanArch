using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyJoiningDate
{
    public record GetEmployeesByJoiningDateQuery(DateTime joiningDateFloor,DateTime joiningDateCeiling) : IRequest<APIResponse<IEnumerable<Employee>>>;

    public class GetEmployeesByJoiningDateQueryHandler : IRequestHandler<GetEmployeesByJoiningDateQuery, APIResponse<IEnumerable<Employee>>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeesByJoiningDateQuery> _validator;

        public GetEmployeesByJoiningDateQueryHandler(IGenericRepository<Employee> repository, IValidator<GetEmployeesByJoiningDateQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<IEnumerable<Employee>>> Handle(GetEmployeesByJoiningDateQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var spec = new EmployeesByJoiningDateSpecification(request.joiningDateFloor,request.joiningDateCeiling);
            var employees = await _repository.GetAllAsync(spec, cancellationToken);


            //var employees = await _repository.GetAllAsync();

            if (employees != null && employees.Any())
            {
                return APIResponse<IEnumerable<Employee>>.Success(employees, "Employees within the filter range fetched successfully.");
            }

            return APIResponse<IEnumerable<Employee>>.Failure("No employees found for the given filter.", HttpStatusCode.NotFound);
        }
    }
}
