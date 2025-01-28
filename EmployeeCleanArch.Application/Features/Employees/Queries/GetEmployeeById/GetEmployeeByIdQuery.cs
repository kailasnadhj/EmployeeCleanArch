using MediatR;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Application.Common.APIResponse;
using System.Net;
using FluentValidation;
using Ardalis.Specification;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById
{
    //public record GetEmployeeByIdQuery(long id) : IRequest<APIResponse<Employee>>;
    public class GetEmployeeByIdQuery : IRequest<APIResponse<Employee>>
    {
        public long id { get; set; }

        public GetEmployeeByIdQuery(long id)
        {
            this.id = id;
        }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<GetEmployeeByIdQuery> _validator;

        public GetEmployeeByIdQueryHandler(IGenericRepository<Employee> repository,IValidator<GetEmployeeByIdQuery> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var employeeData = await _repository.GetByIdAsync(request.id);
            if (employeeData != null)
            {
                return APIResponse<Employee>.Success(employeeData, "Employee fetched successfully.");
            }
            return APIResponse<Employee>.Failure("Employee with the specified ID not found.", HttpStatusCode.NotFound);

        }
    }
}
