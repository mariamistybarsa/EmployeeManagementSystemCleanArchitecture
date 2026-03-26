using FluentValidation;
using EmployeeManagement.Application.DTO.Request.Auth;

namespace EmployeeManagement.Application.Validators.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        // 👤 Employee Info
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(g => g == "Male" || g == "Female" || g == "Other")
            .WithMessage("Gender must be Male, Female, or Other");

        RuleFor(x => x.JoinDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Join date cannot be in the future");

        RuleFor(x => x.DOB)
            .NotEmpty()
            .LessThan(DateTime.UtcNow.AddYears(-18))
            .WithMessage("Employee must be at least 18 years old");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?\d{10,15}$")
            .WithMessage("Invalid phone number");

        RuleFor(x => x.PresentAddress)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required");

        RuleFor(x => x.DesignationId)
            .NotEmpty()
            .WithMessage("Designation is required");

        RuleFor(x => x.BaseSalary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0");

        // 🔐 Auth Info
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role is required");

        RuleFor(x => x.UserName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.UserName));
    }
}