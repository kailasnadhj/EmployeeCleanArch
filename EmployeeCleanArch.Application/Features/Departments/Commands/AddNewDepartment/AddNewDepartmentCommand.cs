using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment
{
    //We use class
    public record AddNewDepartmentCommand(CreateDepartmentDTO departmentDTO) : IRequest<APIResponse<Department>>;

    public class AddNewDepartmentCommandHandler : IRequestHandler<AddNewDepartmentCommand, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IValidator<CreateDepartmentDTO> _validator;

        public AddNewDepartmentCommandHandler(IGenericRepository<Department> repository, IValidator<CreateDepartmentDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Department>> Handle(AddNewDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.departmentDTO);

            /*if (!validationResult.IsValid)
            {
                return APIResponse<Department>.Failure(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    System.Net.HttpStatusCode.BadRequest
                );
            }*/
            var departmentEntity = request.departmentDTO.Adapt<Department>();
            departmentEntity.CreatedDate = DateTime.Now;

            await _repository.AddAsync(departmentEntity);
            return APIResponse<Department>.Success(departmentEntity, "Department created successfully.");
        }
    }
}
