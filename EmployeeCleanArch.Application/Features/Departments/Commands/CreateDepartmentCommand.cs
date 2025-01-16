/*using EmployeeCleanArch.Application.Common.APIResponse;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace EmployeeCleanArch.Application.Features.Departments.Commands
{
    public record CreateDepartmentCommand : IRequest<Result<int>>, IMapFrom<Department>
    {
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public int MaxCapacity { get; set; }
        public string Status { get; set; }
    }

    internal class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                DepartmentName = command.DepartmentName,
                Location = command.Location,
                MaxCapacity = command.MaxCapacity,
                Status = command.Status
            };

            await _unitOfWork.Repository<Department>().AddAsync(department);
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(department.Id, "Department Created.");
        }
    }
}
*/