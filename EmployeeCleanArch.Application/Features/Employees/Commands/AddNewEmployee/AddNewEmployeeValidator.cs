using EmployeeCleanArch.Application.DTOs;
using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Departments.Commands.AddNewDepartment
{
    public class AddNewEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public AddNewEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 25).WithMessage("First name should be between 1 and 25 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 25).WithMessage("Last name should be between 1 and 25 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.") 
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(phone => phone.ToString().Length == 10).WithMessage("Phone number must be exactly 10 digits.");

            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("Designation is required.")
                .Length(2, 100).WithMessage("Designation must be between 2 and 100 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Gender)
                .Must(gender => gender == "male" || gender == "female" || gender == "other")
                .WithMessage("Gender must be 'male', 'female', or 'other'.")
                .NotEmpty().WithMessage("Gender is required.");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Nationality is required.")
                .Length(2, 50).WithMessage("Nationality must be between 2 and 50 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.DateOfJoining)
                .NotEmpty().WithMessage("Date of joining is required.")
                .LessThan(DateTime.Today).WithMessage("Date of joining must be in the past.");

            RuleFor(x => x.DateOfDeparture)
                .GreaterThan(x => x.DateOfJoining).When(x => x.DateOfDeparture.HasValue).WithMessage("Date of departure must be after the date of joining.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Department ID must be greater than 0.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.")
                .Length(3, 50).WithMessage("CreatedBy must be between 3 and 50 characters.");
        }
    }
}
