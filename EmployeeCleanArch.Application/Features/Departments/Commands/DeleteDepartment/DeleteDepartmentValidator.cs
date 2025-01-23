using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

        }
    }
}
