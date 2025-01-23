using EmployeeCleanArch.Application.Features.Employees.Queries.GetEmployeeById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
