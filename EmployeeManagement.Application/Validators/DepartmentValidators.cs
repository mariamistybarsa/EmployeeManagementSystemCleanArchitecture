using FluentValidation;
using EmployeeManagement.Application.DTO.Request.Department;
using EmployeeManagement.Persistence.UnitOfWork;


namespace EmployeeManagement.Application.Validators;

public class CreateDepartmentValidator : AbstractValidator<DepartmentRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters")
            .MustAsync(BeUniqueName).WithMessage("Department name already exists");

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.Departments.GetAllAsync();

        return !list.Any(x =>
            x.Name.ToLower() == name.ToLower() &&
            !x.IsDeleted);
    }
}