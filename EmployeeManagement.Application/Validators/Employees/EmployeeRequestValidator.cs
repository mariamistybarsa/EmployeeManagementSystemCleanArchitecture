using EmployeeManagement.Application.DTO.Request.Employees;
using EmployeeManagement.Application.Validators.Common;
using FluentValidation;

namespace EmployeeManagement.Application.Validators.Employees;

public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
{
    public EmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email)
            .MustAsync(async (email, _) =>
            {
                var errors = await CommonValidator.ValidateEmailAsync(email);
                return !errors.Any();
            });

        RuleFor(x => x.Password)
            .MustAsync(async (password, _) =>
            {
                var errors = await CommonValidator.ValidatePasswordAsync(password);
                return !errors.Any();
            });
    }
}