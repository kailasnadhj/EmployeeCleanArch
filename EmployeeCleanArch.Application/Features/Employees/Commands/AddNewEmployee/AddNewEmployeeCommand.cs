using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using EmployeeCleanArch.Domain.Enums;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

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
            if (!Enum.TryParse<Genders>(request.employeeDTO.Gender, true, out var genderEnum))
            {
                return APIResponse<Employee>.Failure("Invalid gender value. Must be 'male', 'female', or 'other'.",HttpStatusCode.BadRequest);
            }

            employeeEntity.Gender = genderEnum;
            employeeEntity.CreatedDate = DateTime.Now;

            await _repository.AddAsync(employeeEntity);
            return APIResponse<Employee>.Success(employeeEntity, "Employee created successfully.");
        }
    }
}
