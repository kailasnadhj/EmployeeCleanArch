using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(UpdateEmployeeDTO employeeDTO, long id) : IRequest<APIResponse<Employee>>;

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<UpdateEmployeeCommand> _validator;

        public UpdateEmployeeCommandHandler(IGenericRepository<Employee> repository, IValidator<UpdateEmployeeCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            Employee employeeEntity = await _repository.GetByIdAsync(request.id);

            if (employeeEntity == null)
            {
                return APIResponse<Employee>.Failure("Employee of the specified ID not found.", HttpStatusCode.NotFound);
            }

            request.employeeDTO.Adapt(employeeEntity);

            employeeEntity.CreatedDate = employeeEntity.CreatedDate ?? DateTime.UtcNow;

            employeeEntity.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(employeeEntity);

            return APIResponse<Employee>.Success(employeeEntity, "Employee updated successfully.");
        }



    }
}
