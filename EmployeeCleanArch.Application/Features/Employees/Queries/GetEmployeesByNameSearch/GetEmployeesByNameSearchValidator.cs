using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeesByNameSearch
{
    public class GetEmployeesByNameSearchQueryValidator : AbstractValidator<GetEmployeesByNameSearchQuery>
    {
        public GetEmployeesByNameSearchQueryValidator()
        {
            RuleFor(x => x.searchKey)
                .NotEmpty().WithMessage("Search key cannot be empty")
                .Length(2, 50).WithMessage("Search key must be between 2 and 50 characters.");
        }
    }
}
