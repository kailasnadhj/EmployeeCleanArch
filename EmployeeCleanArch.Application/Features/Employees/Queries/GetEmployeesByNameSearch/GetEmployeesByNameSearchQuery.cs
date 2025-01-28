using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Specifications;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesByNameSearch
{
    //public record GetEmployeesByNameSearchQuery(string searchKey) : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>;
    public class GetEmployeesByNameSearchQuery : IRequest<APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        public string searchKey { get; set; }

        public GetEmployeesByNameSearchQuery(string searchKey)
        {
            this.searchKey = searchKey;
        }
    }

    public class GetEmployeesByNameSearchQueryHandler : IRequestHandler<GetEmployeesByNameSearchQuery, APIResponse<IEnumerable<GetEmployeeDTO>>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeesByNameSearchQuery> _validator;

        public GetEmployeesByNameSearchQueryHandler(IGenericRepository<Employee> repository, IValidator<GetEmployeesByNameSearchQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<IEnumerable<GetEmployeeDTO>>> Handle(GetEmployeesByNameSearchQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var spec = new EmployeesByNameSearchSpecification(request.searchKey);
            var employeesList = await _repository.GetAllAsync(spec, cancellationToken);
            var employees = employeesList.Adapt<IEnumerable<GetEmployeeDTO>>();


            //var employees = await _repository.GetAllAsync();

            if (employees != null)
            {
                return APIResponse<IEnumerable<GetEmployeeDTO>>.Success(employees, "Employees within the filter range fetched successfully.");
            }

            return APIResponse<IEnumerable<GetEmployeeDTO>>.Failure("No employees found for the given filter.", HttpStatusCode.NotFound);
        }
    }
}
