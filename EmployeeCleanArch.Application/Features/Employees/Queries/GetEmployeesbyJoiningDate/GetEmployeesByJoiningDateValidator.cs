using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyJoiningDate
{
    public class GetEmployeesByJoiningDateValidator : AbstractValidator<GetEmployeesByJoiningDateQuery>
    {
        public GetEmployeesByJoiningDateValidator()
        {
            RuleFor(x => x.joiningDateFloor)
                .NotEmpty().WithMessage("Joining date floor cannot be empty")
                .Must(date => date != default(DateTime)).WithMessage("Joining date floor is not a valid date");

            RuleFor(x => x.joiningDateCeiling)
                .NotEmpty().WithMessage("Joining date ceiling cannot be empty")
                .Must(date => date != default(DateTime)).WithMessage("Joining date ceiling is not a valid date");

            RuleFor(x => x)
                .Must(x => x.joiningDateFloor <= x.joiningDateCeiling)
                .WithMessage("Joining date floor cannot be later than joining date ceiling");

        }
    }
}
