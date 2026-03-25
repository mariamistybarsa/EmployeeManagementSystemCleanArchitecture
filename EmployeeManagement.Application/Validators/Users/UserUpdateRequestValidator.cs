using EmployeeManagement.Application.DTO.Request.Users;
using FluentValidation;

namespace EmployeeManagement.Application.Validators.Users;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
      
        RuleFor(x => x.UserName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.UserName));

      
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .When(x => x.RoleId.HasValue);

       
        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .When(x => x.EmployeeId.HasValue);
    }
}