using EmployeeManagement.Application.DTO.Request.Department;
using EmployeeManagement.Application.Mappers.Departments;
using EmployeeManagement.Application.Services.Interfaces.Department;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;

namespace EmployeeManagement.Application.Services.Implementations.Department;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DepartmentRequest> _validator;

    public DepartmentService(IUnitOfWork unitOfWork, IValidator<DepartmentRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<StandardResponse> AddAsync(DepartmentRequest request)
    {
       
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("Department", errors);
        }

        var entity = DepartmentMapper.MapRequestToEntity(request);

        await _unitOfWork.Departments.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var data = DepartmentMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Department created successfully", data);
    }

    // 🔹 GET ALL
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.Departments.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(DepartmentMapper.MapEntityToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No departments found");

        return ApiResponseHelper.Success("Departments retrieved successfully", data);
    }

    // 🔹 GET BY ID
    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.Departments.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Department not found");

        var data = DepartmentMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Department retrieved successfully", data);
    }

    // 🔹 UPDATE
    public async Task<StandardResponse> UpdateAsync(Guid id, DepartmentRequest request)
    {
        // ✅ Validation added
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("Department", errors);
        }

        var entity = await _unitOfWork.Departments.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Department not found");

        DepartmentMapper.MapRequestToUpdatedEntity(entity, request);

        _unitOfWork.Departments.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Department updated successfully");
    }

    // 🔹 DELETE (Soft Delete)
    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Departments.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Department not found");

        entity.IsDeleted = true;

        _unitOfWork.Departments.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Department deleted successfully");
    }
}