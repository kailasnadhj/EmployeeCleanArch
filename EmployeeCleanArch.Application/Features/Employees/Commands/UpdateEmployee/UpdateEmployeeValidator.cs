using FluentValidation;

namespace EmployeeCleanArch.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage("ID cannot be 0 or less.");

            RuleFor(x => x.employeeDTO.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 25).WithMessage("First name should be between 1 and 25 characters.");

            RuleFor(x => x.employeeDTO.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 25).WithMessage("Last name should be between 1 and 25 characters.");

            RuleFor(x => x.employeeDTO.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.employeeDTO.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(phone => phone.ToString().Length == 10).WithMessage("Phone number must be exactly 10 digits.");

            RuleFor(x => x.employeeDTO.Designation)
                .NotEmpty().WithMessage("Designation is required.")
                .Length(2, 100).WithMessage("Designation must be between 2 and 100 characters.");

            RuleFor(x => x.employeeDTO.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.employeeDTO.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(gender => gender == "male" || gender == "female" || gender == "other").WithMessage("Gender must be either 'male', 'female', or 'other'.");

            RuleFor(x => x.employeeDTO.Nationality)
                .NotEmpty().WithMessage("Nationality is required.")
                .Length(2, 50).WithMessage("Nationality must be between 2 and 50 characters.");

            RuleFor(x => x.employeeDTO.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.employeeDTO.DateOfJoining)
                .NotEmpty().WithMessage("Date of joining is required.")
                .LessThan(DateTime.Today).WithMessage("Date of joining must be in the past.");

            RuleFor(x => x.employeeDTO.DateOfDeparture)
                .GreaterThan(x => x.employeeDTO.DateOfJoining).When(x => x.employeeDTO.DateOfDeparture.HasValue).WithMessage("Date of departure must be after the date of joining.");

            RuleFor(x => x.employeeDTO.DepartmentId)
                .GreaterThan(0).WithMessage("Department ID must be greater than 0.");

            RuleFor(x => x.employeeDTO.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required.")
                .Length(3, 50).WithMessage("UpdatedBy must be between 3 and 50 characters.");
        }
    }
}
