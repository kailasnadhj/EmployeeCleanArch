using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.PurgeEmployee
{
    //public record PurgeEmployeeCommand(long id) : IRequest<APIResponse<Employee>>;
    public class PurgeEmployeeCommand : IRequest<APIResponse<Employee>>
    {
        public long id { get; set; }

        public PurgeEmployeeCommand(long id)
        {
            this.id = id;
        }
    }

    public class PurgeEmployeeCommandHandler : IRequestHandler<PurgeEmployeeCommand, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<PurgeEmployeeCommand> _validator;

        public PurgeEmployeeCommandHandler(IGenericRepository<Employee> repository, IValidator<PurgeEmployeeCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Employee>> Handle(PurgeEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var employeeEntity = await _repository.GetByIdAsync(request.id);

            if (employeeEntity == null)
            {
                return APIResponse<Employee>.Failure("Employee of the specified ID not found.", HttpStatusCode.NotFound);

            }

            await _repository.DeleteAsync(employeeEntity);
            return APIResponse<Employee>.Success(employeeEntity, "Employee data purged successfully.");
        }
    }
}
