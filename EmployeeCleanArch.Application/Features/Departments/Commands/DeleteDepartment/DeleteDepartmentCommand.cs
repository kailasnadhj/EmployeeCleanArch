using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentCommand(int id) : IRequest<APIResponse<Department>>;

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IValidator<DeleteDepartmentCommand> _validator;

        public DeleteDepartmentCommandHandler(IGenericRepository<Department> repository, IValidator<DeleteDepartmentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Department>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var departmentEntity = await _repository.GetByIdAsync(request.id);

            if (departmentEntity == null)
            {
                return APIResponse<Department>.Failure("Department of the specified ID found.", HttpStatusCode.NotFound);

            }
            departmentEntity.IsDeleted = true;
            departmentEntity.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(departmentEntity);
            return APIResponse<Department>.Success(departmentEntity, "Department deleted successfully.");
        }
    }
}
