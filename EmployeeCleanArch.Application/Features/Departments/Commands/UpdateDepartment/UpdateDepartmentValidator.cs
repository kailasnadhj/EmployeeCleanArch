using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

            RuleFor(x => x.departmentDTO.DepartmentName)
                .NotEmpty().WithMessage("Department name is required.")
                .Length(2, 20).WithMessage("Department name should be between 2 and 20 characters.");

            RuleFor(x => x.departmentDTO.Location)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(x => x.departmentDTO.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.departmentDTO.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy Username is required.");

            RuleFor(x => x.departmentDTO.MaxCapacity)
                .GreaterThanOrEqualTo(1).WithMessage("Maximum capacity cannot be zero.");
        }
    }
}
