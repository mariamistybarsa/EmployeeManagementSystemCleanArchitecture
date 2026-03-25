using EmployeeManagement.Application.DTO.Request.Designations;
using EmployeeManagement.Application.Mappers;
using EmployeeManagement.Application.Mappers.Designations;
using EmployeeManagement.Application.Services.Interfaces.Designations;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;

namespace EmployeeManagement.Application.Services.Implementations.Designations;

public class DesignationService : IDesignationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DesignationRequest> _validator;

    public DesignationService(IUnitOfWork unitOfWork, IValidator<DesignationRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

  
    public async Task<StandardResponse> AddAsync(DesignationRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("Designation", errors);
        }

        var entity = DesignationMapper.MapRequestToEntity(request);

        await _unitOfWork.Designations.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var data = DesignationMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Designation created successfully", data);
    }

    // 🔹 GET ALL
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.Designations.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(DesignationMapper.MapEntityToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No designations found");

        return ApiResponseHelper.Success("Designations retrieved successfully", data);
    }

    // 🔹 GET BY ID
    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.Designations.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Designation not found");

        var data = DesignationMapper.MapEntityToResponse(entity);

        return ApiResponseHelper.Success("Designation retrieved successfully", data);
    }

    // 🔹 UPDATE
    public async Task<StandardResponse> UpdateAsync(Guid id, DesignationRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("Designation", errors);
        }

        var entity = await _unitOfWork.Designations.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Designation not found");

        // update mapping
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.UpdatedBy = Guid.Empty;

        _unitOfWork.Designations.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Designation updated successfully");
    }

    // 🔹 DELETE (Soft Delete)
    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Designations.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Designation not found");

        entity.IsDeleted = true;

        _unitOfWork.Designations.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Designation deleted successfully");
    }
}