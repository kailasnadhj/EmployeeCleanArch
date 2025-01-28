using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment
{
    //public record AddNewDepartmentCommand(CreateDepartmentDTO departmentDTO) : IRequest<APIResponse<Department>>;
    public class AddNewDepartmentCommand : IRequest<APIResponse<Department>>
    {
        public CreateDepartmentDTO departmentDTO { get; set; }

        public AddNewDepartmentCommand(CreateDepartmentDTO departmentDTO)
        {
            this.departmentDTO = departmentDTO;
        }
    }

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

            var departmentEntity = request.departmentDTO.Adapt<Department>();
            departmentEntity.CreatedDate = DateTime.Now;

            await _repository.AddAsync(departmentEntity);
            return APIResponse<Department>.Success(departmentEntity, "Department created successfully.");
        }
    }
}
