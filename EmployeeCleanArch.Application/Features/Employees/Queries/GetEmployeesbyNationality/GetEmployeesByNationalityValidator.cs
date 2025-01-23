using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesbyNationality
{
    public class GetEmployeeByNationalityValidator : AbstractValidator<GetEmployeesByNationalityQuery>
    {
        public GetEmployeeByNationalityValidator()
        {
            RuleFor(x => x.nationality)
                .NotEmpty().WithMessage("Nationality cannot be empty");

        }
    }
}
