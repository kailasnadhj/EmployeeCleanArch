using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.AddNewEmployee
{
    public record AddNewEmployeeCommand(CreateEmployeeDTO employeeDTO) : IRequest<APIResponse<Employee>>;

    public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, APIResponse<Employee>>
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IValidator<CreateEmployeeDTO> _validator;

        public AddNewEmployeeCommandHandler(IGenericRepository<Employee> repository, IValidator<CreateEmployeeDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Employee>> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.employeeDTO);

            var employeeEntity = request.employeeDTO.Adapt<Employee>();
            employeeEntity.CreatedDate = DateTime.Now;

            await _repository.AddAsync(employeeEntity);
            return APIResponse<Employee>.Success(employeeEntity, "Department created successfully.");
        }
    }
}
