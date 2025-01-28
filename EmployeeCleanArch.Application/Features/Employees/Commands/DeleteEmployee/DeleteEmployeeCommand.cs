using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.DeleteEmployee
{
    //public record DeleteEmployeeCommand(long id) : IRequest<APIResponse<Employee>>;
    public class DeleteEmployeeCommand : IRequest<APIResponse<Employee>>
    {
        public long id { get; set; }

        public DeleteEmployeeCommand(long id)
        {
            this.id = id;
        }
    }


    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<DeleteEmployeeCommand> _validator;

        public DeleteEmployeeCommandHandler(IGenericRepository<Employee> repository, IValidator<DeleteEmployeeCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Employee>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var employeeEntity = await _repository.GetByIdAsync(request.id);

            if (employeeEntity == null)
            {
                return APIResponse<Employee>.Failure("Employee of the specified ID not found.", HttpStatusCode.NotFound);

            }

            employeeEntity.IsDeleted = true;
            employeeEntity.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(employeeEntity);
            return APIResponse<Employee>.Success(employeeEntity, "Employee deleted successfully.");
        }
    }
}
