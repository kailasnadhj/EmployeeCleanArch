using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.PurgeEmployee
{
    public class PurgeEmployeeValidator : AbstractValidator<PurgeEmployeeCommand>
    {
        public PurgeEmployeeValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

        }
    }
}
