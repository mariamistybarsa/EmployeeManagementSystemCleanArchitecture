using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Application.DTO.Request.Employees;
using EmployeeManagement.Application.Services.Interfaces.Employees;

namespace EmployeeManagement.WebAPI.Controllers.Employees;

[ApiController]
[Route("api/employees")]
[Authorize] 
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

 
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeRequest request)
    {
        var result = await _service.AddAsync(request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var result = await _service.GetAllAsync(pageNumber, pageSize);
        return StatusCode(int.Parse(result.StatusCode), result);
    }
    
    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, EmployeeUpdateRequest request)
    {
        var result = await _service.UpdateEmployeeAsync(id, request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }
}