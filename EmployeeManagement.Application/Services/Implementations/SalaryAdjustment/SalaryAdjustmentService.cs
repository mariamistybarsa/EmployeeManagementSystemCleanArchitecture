using System.Security.Claims;
using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;
using EmployeeManagement.Application.Mappers.SalaryAdjustments;
using EmployeeManagement.Application.Services.Interfaces.SalaryAdjustment;
using EmployeeManagement.Application.Validators.SalaryAdjustment;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Services.Implementations.SalaryAdjustment;
public class SalaryAdjustmentService : ISalaryAdjustmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IValidator<SalaryAdjustmentRequest> _validator;
    private readonly IValidator<SalaryAdjustmentUpdateRequest> _updateValidator;

    public SalaryAdjustmentService(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContext,
        IValidator<SalaryAdjustmentRequest> validator,
        IValidator<SalaryAdjustmentUpdateRequest> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
        _validator = validator;
        _updateValidator = updateValidator;
    }
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.SalaryAdjustments.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(SalaryAdjustmentMapper.ToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No roles found");

        return ApiResponseHelper.Success("Roles retrieved successfully", data);
    }

    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.SalaryAdjustments.GetByIdAsync(id);

        if (entity == null)
            return ApiResponseHelper.NotFound("Adjustment not found");

        return ApiResponseHelper.Success("Adjustment", entity);
    }

    public async Task<StandardResponse> AddAsync(SalaryAdjustmentRequest request)
    {
        try
        {
          
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                    .ToList();

                return ApiResponseHelper.ValidationError("Validation failed", errors);
            }

            
            var userIdClaim = _httpContext.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return ApiResponseHelper.Unauthorized("User not authenticated");
            }

            var userId = Guid.Parse(userIdClaim);
            var entity = SalaryAdjustmentMapper.ToEntity(request, userId);

            await _unitOfWork.SalaryAdjustments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var response = SalaryAdjustmentMapper.ToResponse(entity);

            return ApiResponseHelper.Success("Adjustment created successfully", response);
        }
        catch (Exception ex)
        {
            return ApiResponseHelper.FailedToCreate(ex.Message, new List<ErrorDetails>());
        }
    }

    public Task<StandardResponse> UpdateAsync(Guid id, SalaryAdjustmentRequest entity)
    {
        throw new NotImplementedException();
    }


    public async Task<StandardResponse> UpdateSalaryAdjustmentAsync(Guid id, SalaryAdjustmentUpdateRequest request)
    {
        try
        {
            var validation = await _updateValidator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                    .ToList();

                return ApiResponseHelper.ValidationError("Validation failed", errors);
            }

            var entity = await _unitOfWork.SalaryAdjustments.GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                return ApiResponseHelper.NotFound("Adjustment not found");

            var userIdClaim = _httpContext.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return ApiResponseHelper.Unauthorized("User not authenticated");

            var userId = Guid.Parse(userIdClaim);

            SalaryAdjustmentMapper.ToUpdate(entity, request, userId);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponseHelper.Success("Adjustment updated successfully",
                SalaryAdjustmentMapper.ToResponse(entity));
        }
        catch (Exception ex)
        {
            return ApiResponseHelper.FailedToUpdate(ex.Message, new List<ErrorDetails>(), null);
        }
    }

    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.SalaryAdjustments.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Employee not found");

        entity.IsDeleted = true;

        _unitOfWork.SalaryAdjustments.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Employee deleted successfully");
    }

  
}