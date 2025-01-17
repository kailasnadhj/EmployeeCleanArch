using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using MediatR;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Commands
{
    public record UpdateDepartmentCommand(UpdateDepartmentDTO departmentDTO,int id) : IRequest<APIResponse<Department>>;

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IValidator<UpdateDepartmentCommand> _validator;

        public UpdateDepartmentCommandHandler(IGenericRepository<Department> repository, IValidator<UpdateDepartmentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Department>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return APIResponse<Department>.Failure(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Department departmentEntity = new Department();
            departmentEntity = await _repository.GetByIdAsync(request.id);

            if (departmentEntity == null)
            {
                return APIResponse<Department>.Failure("Department of the specified ID found.", HttpStatusCode.NotFound);

            }

            departmentEntity.DepartmentName = request.departmentDTO.DepartmentName;
            departmentEntity.MaxCapacity = request.departmentDTO.MaxCapacity;
            departmentEntity.Status = request.departmentDTO.Status;
            departmentEntity.Location = request.departmentDTO.Location;
            departmentEntity.UpdatedBy = request.departmentDTO.UpdatedBy;
            departmentEntity.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(departmentEntity);
            return APIResponse<Department>.Success(departmentEntity, "Department updated successfully.");
        }
    }
}
