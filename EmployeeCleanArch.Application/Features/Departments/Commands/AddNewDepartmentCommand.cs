using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeCleanArch.Application.Features.Departments.Commands
{
    public record AddNewDepartmentCommand(CreateDepartmentDTO departmentDTO) : IRequest<Department>;

    public class AddNewDepartmentCommandHandler : IRequestHandler<AddNewDepartmentCommand, Department>
    {
        private readonly IGenericRepository<Department> _repository;

        public AddNewDepartmentCommandHandler(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<Department> Handle(AddNewDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentEntity = new Department
            {
                DepartmentName = request.departmentDTO.DepartmentName,
                MaxCapacity = request.departmentDTO.MaxCapacity,
                Status = request.departmentDTO.Status,
                Location = request.departmentDTO.Location,
                CreatedBy = request.departmentDTO.CreatedBy,
                CreatedDate = DateTime.Now
            };

            return await _repository.AddAsync(departmentEntity);
        }
    }
}
