using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

        }
    }
}
