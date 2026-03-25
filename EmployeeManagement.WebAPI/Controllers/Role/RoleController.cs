using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Application.DTO.Request.Roles;
using EmployeeManagement.Application.Services.Interfaces.Roles;

namespace EmployeeManagement.WebAPI.Controllers.Role;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleRequest request)
    {
        var result = await _service.AddAsync(request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var result = await _service.GetAllAsync(pageNumber, pageSize);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, RoleUpdateRequest request)
    {
        var result = await _service.UpdateRoleAsync(id, request);
        return StatusCode(int.Parse(result.StatusCode), result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        return StatusCode(int.Parse(result.StatusCode), result);
    }
}