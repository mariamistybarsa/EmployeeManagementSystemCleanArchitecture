using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;
using EmployeeManagement.Application.DTO.Response.SalaryAdjustment;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.SalaryAdjustments;

public static class SalaryAdjustmentMapper
{
    
    public static SalaryAdjustment ToEntity(SalaryAdjustmentRequest request, Guid userId)
    {
        return new SalaryAdjustment
        {
            Id = Guid.NewGuid(),
            EmpId = request.EmpId,
            AdjustmentType = request.AdjustmentType,
            AdjustmentName = request.AdjustmentName,
            Amount = request.Amount,
            Reason = request.Reason,
            EffectiveMonth = request.EffectiveMonth,
            EffectiveYear = request.EffectiveYear,

            IsActive = true,
            IsDeleted = false,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };
    }
    
    public static SalaryAdjustmentResponse ToResponse(SalaryAdjustment entity)
    {
        return new SalaryAdjustmentResponse
        {
            Id = entity.Id,
            EmpId = entity.EmpId,
            AdjustmentType = entity.AdjustmentType,
            AdjustmentName = entity.AdjustmentName,
            Amount = entity.Amount,
            Reason = entity.Reason,
            EffectiveMonth = entity.EffectiveMonth,
            EffectiveYear = entity.EffectiveYear
        };
    }
   
        public static void ToUpdate(
            SalaryAdjustment entity,
            SalaryAdjustmentUpdateRequest request,
            Guid userId)
        {
            if (request.EmpId.HasValue)
                entity.EmpId = request.EmpId.Value;

            if (!string.IsNullOrWhiteSpace(request.AdjustmentType))
                entity.AdjustmentType = request.AdjustmentType;

            if (!string.IsNullOrWhiteSpace(request.AdjustmentName))
                entity.AdjustmentName = request.AdjustmentName;

            if (request.Amount.HasValue)
                entity.Amount = request.Amount.Value;

            if (!string.IsNullOrWhiteSpace(request.Reason))
                entity.Reason = request.Reason;

            if (request.EffectiveMonth.HasValue)
                entity.EffectiveMonth = request.EffectiveMonth.Value;

            if (request.EffectiveYear.HasValue)
                entity.EffectiveYear = request.EffectiveYear.Value;
        }
    }
