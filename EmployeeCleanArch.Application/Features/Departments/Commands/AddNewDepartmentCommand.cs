using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using MediatR;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCleanArch.Application.Features.Departments.Commands
{
    public record AddNewDepartmentCommand(CreateDepartmentDTO departmentDTO) : IRequest<APIResponse<Department>>;

    public class AddNewDepartmentCommandHandler : IRequestHandler<AddNewDepartmentCommand, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IValidator<CreateDepartmentDTO> _validator;

        public AddNewDepartmentCommandHandler(IGenericRepository<Department> repository,IValidator<CreateDepartmentDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Department>> Handle(AddNewDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.departmentDTO);

            if (!validationResult.IsValid)
            {
                return APIResponse<Department>.Failure(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            var departmentEntity = new Department
            {
                DepartmentName = request.departmentDTO.DepartmentName,
                MaxCapacity = request.departmentDTO.MaxCapacity,
                Status = request.departmentDTO.Status,
                Location = request.departmentDTO.Location,
                CreatedBy = request.departmentDTO.CreatedBy,
                CreatedDate = DateTime.Now
            };

            await _repository.AddAsync(departmentEntity);
            return APIResponse<Department>.Success(departmentEntity, "Department fetched successfully.");
        }
    }
}
