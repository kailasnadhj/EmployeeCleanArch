using EmployeeCleanArch.Application.DTOs;
using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Departments.Commands
{
    public class AddNewDepartmentValidator : AbstractValidator<CreateDepartmentDTO>
    {
        public AddNewDepartmentValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Department name is required.")
                .Length(2, 20).WithMessage("Department name should be between 2 and 20 characters.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Createdby Username is required.");

            RuleFor(x => x.MaxCapacity)
                .GreaterThanOrEqualTo(1).WithMessage("Maximum capacity cannot be zero.");
        }
    }
}
