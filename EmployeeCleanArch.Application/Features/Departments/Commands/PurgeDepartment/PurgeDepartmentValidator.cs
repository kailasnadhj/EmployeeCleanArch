using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.PurgeDepartment
{
    public class PurgeDepartmentValidator : AbstractValidator<PurgeDepartmentCommand>
    {
        public PurgeDepartmentValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

        }
    }
}
