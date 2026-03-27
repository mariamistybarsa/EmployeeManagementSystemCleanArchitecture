using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;

namespace EmployeeManagement.Application.Validators.SalaryAdjustment;

using FluentValidation;

public class SalaryAdjustmentValidator : AbstractValidator<SalaryAdjustmentRequest>
{
    public SalaryAdjustmentValidator()
    {
        RuleFor(x => x.EmpId).NotEmpty();

        RuleFor(x => x.AdjustmentType)
            .NotEmpty()
            .Must(x => x == "Allowance" || x == "Deduction")
            .WithMessage("AdjustmentType must be Allowance or Deduction");

        RuleFor(x => x.AdjustmentName).NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.EffectiveMonth)
            .InclusiveBetween(1, 12);

        RuleFor(x => x.EffectiveYear)
            .GreaterThan(2000);
    }
}