using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Application.DTO.Request.SalaryAdjustment;
using EmployeeManagement.Application.Services.Interfaces.SalaryAdjustment;

namespace EmployeeManagement.WebAPI.Controllers.SalaryAdjustment;

[ApiController]
[Route("api/adjustments")]
[Authorize] 
public class SalaryAdjustmentController(ISalaryAdjustmentService service) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(SalaryAdjustmentRequest request)
    {
        var result = await service.AddAsync(request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var result = await service.GetAllAsync(pageNumber, pageSize);
        return StatusCode(int.Parse(result.StatusCode), result);
    }


    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, SalaryAdjustmentUpdateRequest request)
    {
        var result = await service.UpdateSalaryAdjustmentAsync(id, request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.DeleteAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }
}