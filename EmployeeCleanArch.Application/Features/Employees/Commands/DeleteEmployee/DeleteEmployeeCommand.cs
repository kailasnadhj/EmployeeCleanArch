using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(long id) : IRequest<APIResponse<Employee>>;

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
                return APIResponse<Employee>.Failure("Department of the specified ID found.", HttpStatusCode.NotFound);

            }

            await _repository.DeleteAsync(employeeEntity);
            return APIResponse<Employee>.Success(employeeEntity, "Department deleted successfully.");
        }
    }
}
