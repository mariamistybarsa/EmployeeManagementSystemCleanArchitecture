using EmployeeManagement.Application.DTO.Request.Employees;
using FluentValidation;

namespace EmployeeManagement.Application.Validators.Employees;
public class EmployeeUpdateRequestValidator : AbstractValidator<EmployeeUpdateRequest>
{
    public EmployeeUpdateRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.BaseSalary)
            .GreaterThanOrEqualTo(0)
            .When(x => x.BaseSalary.HasValue);
    }
}
