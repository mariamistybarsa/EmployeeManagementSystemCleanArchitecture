using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;

namespace EmployeeManagement.Application.Validators.Common;

public static class CommonValidator
{
    public static async Task<List<ErrorDetails>> ValidateEmailAsync(string email)
    {
        var validator = new InlineValidator<string>();
        validator.RuleFor(e => e)
            .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmailFormat);

        var result = await validator.ValidateAsync(email);

        return result.IsValid
            ? new List<ErrorDetails>()
            : result.Errors.Select(e => new ErrorDetails(FieldNames.UserEmail, e.ErrorMessage)).ToList();
    }

    public static async Task<List<ErrorDetails>> ValidatePasswordAsync(string password)
    {
        var validator = new InlineValidator<string>();
        validator.RuleFor(p => p)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(8).WithMessage(ValidationMessages.PasswordMinLength)
            .Matches(@"[A-Z]").WithMessage(ValidationMessages.PasswordUppercase)
            .Matches(@"[a-z]").WithMessage(ValidationMessages.PasswordLowercase)
            .Matches(@"\d").WithMessage(ValidationMessages.PasswordDigit);

        var result = await validator.ValidateAsync(password);

        return result.IsValid
            ? new List<ErrorDetails>()
            : result.Errors.Select(e => new ErrorDetails(FieldNames.Password, e.ErrorMessage)).ToList();
    }
}
