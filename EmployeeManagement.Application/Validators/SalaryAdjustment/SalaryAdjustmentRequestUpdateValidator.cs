using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;
using FluentValidation;

namespace EmployeeManagement.Application.Validators.SalaryAdjustment;

public class SalaryAdjustmentUpdateRequestValidator : AbstractValidator<SalaryAdjustmentUpdateRequest>
{
    public SalaryAdjustmentUpdateRequestValidator()
    {
        RuleFor(x => x.AdjustmentName)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentName));

        RuleFor(x => x.AdjustmentType)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentType));

        RuleFor(x => x.Reason)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .When(x => x.Amount.HasValue);

        RuleFor(x => x.EffectiveMonth)
            .InclusiveBetween(1, 12)
            .When(x => x.EffectiveMonth.HasValue);

        RuleFor(x => x.EffectiveYear)
            .GreaterThan(2000)
            .When(x => x.EffectiveYear.HasValue);

        RuleFor(x => x.EmpId)
            .NotEmpty()
            .When(x => x.EmpId.HasValue);
    }
}