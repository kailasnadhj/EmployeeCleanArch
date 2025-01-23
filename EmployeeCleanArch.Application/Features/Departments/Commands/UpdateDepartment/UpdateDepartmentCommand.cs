using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.UpdateDepartment
{
    public record UpdateDepartmentCommand(UpdateDepartmentDTO departmentDTO, int id) : IRequest<APIResponse<Department>>;

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
            await _validator.ValidateAndThrowAsync(request);

            Department departmentEntity = await _repository.GetByIdAsync(request.id);

            if (departmentEntity == null)
            {
                return APIResponse<Department>.Failure("Department of the specified ID not found.", HttpStatusCode.NotFound);
            }

            request.departmentDTO.Adapt(departmentEntity);  

            departmentEntity.CreatedDate = departmentEntity.CreatedDate ?? DateTime.UtcNow;  

            departmentEntity.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(departmentEntity);

            return APIResponse<Department>.Success(departmentEntity, "Department updated successfully.");
        }



    }
}
