using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Net;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.PurgeDepartment
{
    //public record PurgeDepartmentCommand(long id) : IRequest<APIResponse<Department>>;
    public class PurgeDepartmentCommand : IRequest<APIResponse<Department>>
    {
        public long id { get; set; }

        public PurgeDepartmentCommand(long id)
        {
            this.id = id;
        }
    }

    public class PurgeDepartmentCommandHandler : IRequestHandler<PurgeDepartmentCommand, APIResponse<Department>>
    {
        private readonly IGenericRepository<Department> _repository;
        private readonly IValidator<PurgeDepartmentCommand> _validator;

        public PurgeDepartmentCommandHandler(IGenericRepository<Department> repository, IValidator<PurgeDepartmentCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<APIResponse<Department>> Handle(PurgeDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var employeeEntity = await _repository.GetByIdAsync(request.id);

            if (employeeEntity == null)
            {
                return APIResponse<Department>.Failure("Department of the specified ID not found.", HttpStatusCode.NotFound);

            }

            await _repository.DeleteAsync(employeeEntity);
            return APIResponse<Department>.Success(employeeEntity, "Department data purged successfully.");
        }
    }
}
