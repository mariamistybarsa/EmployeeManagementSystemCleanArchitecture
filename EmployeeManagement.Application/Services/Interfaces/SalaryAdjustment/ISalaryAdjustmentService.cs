using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces.SalaryAdjustment;

public interface ISalaryAdjustmentService : IGenericService<SalaryAdjustmentRequest>
{
    Task<StandardResponse> UpdateSalaryAdjustmentAsync(Guid id, SalaryAdjustmentUpdateRequest request);
}