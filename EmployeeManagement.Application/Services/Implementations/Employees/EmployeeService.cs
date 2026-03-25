using EmployeeManagement.Application.DTO.Request.Employees;
using EmployeeManagement.Application.Mappers.Employees;
using EmployeeManagement.Application.Services.Interfaces.Employees;
using EmployeeManagement.Application.Validators.Common;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;

namespace EmployeeManagement.Application.Services.Implementations.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<EmployeeRequest> _validator;
    private readonly IValidator<EmployeeUpdateRequest> _updatevalidator;
    public EmployeeService(IUnitOfWork unitOfWork, IValidator<EmployeeRequest> validator,IValidator<EmployeeUpdateRequest> updatevalidator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _updatevalidator = updatevalidator;
    }

    public async Task<StandardResponse> AddAsync(EmployeeRequest request)
    {
      
        var validationResult = await _validator.ValidateAsync(request);

        var errors = validationResult.Errors
            .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
            .ToList();

     
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors.Add(new ErrorDetails("Email", "Email is required"));
        }

        
        var emailErrors = await CommonValidator.ValidateEmailAsync(request.Email);
        errors.AddRange(emailErrors);

       
        var passwordErrors = await CommonValidator.ValidatePasswordAsync(request.Password);
        errors.AddRange(passwordErrors);

    
        var existingEmployees = await _unitOfWork.Employees.GetAllAsync();

        if (existingEmployees.Any(x => !x.IsDeleted && x.Email == request.Email))
        {
            errors.Add(new ErrorDetails("Email", "Email already exists"));
        }

   
        if (errors.Any())
            return ApiResponseHelper.ValidationError("Employee", errors);

        var dept = await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId);
        if (dept == null)
            return ApiResponseHelper.NotFound("Department not found");


        var entity = EmployeeMapper.MapRequestToEntity(request);

        await _unitOfWork.Employees.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var data = EmployeeMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Employee created successfully", data);
    }

    public Task<StandardResponse> UpdateAsync(Guid id, EmployeeRequest entity)
    {
        throw new NotImplementedException();
    }

    // 🔹 GET ALL
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.Employees.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(EmployeeMapper.MapEntityToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No employees found");

        return ApiResponseHelper.Success("Employees retrieved successfully", data);
    }

    // 🔹 GET BY ID
    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.Employees.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Employee not found");

        var data = EmployeeMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Employee retrieved successfully", data);
    }

    // 🔹 UPDATE
    public async Task<StandardResponse> UpdateEmployeeAsync(Guid id, EmployeeUpdateRequest request)
    {
        var validationResult = await _updatevalidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("Employee", errors);
        }

        var entity = await _unitOfWork.Employees.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Employee not found");

        EmployeeMapper.MapRequestToUpdatedEntity(entity, request);

        _unitOfWork.Employees.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Employee updated successfully");
    }

    // 🔹 DELETE (Soft Delete)
    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Employees.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Employee not found");

        entity.IsDeleted = true;

        _unitOfWork.Employees.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Employee deleted successfully");
    }
}