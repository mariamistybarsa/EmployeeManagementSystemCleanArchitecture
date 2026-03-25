using EmployeeManagement.Application.DTO.Request.Designations;
using FluentValidation;

namespace EmployeeManagement.Application.Validators;

public class DesignationRequestValidator : AbstractValidator<DesignationRequest>
{
    public DesignationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(250);
    }
}