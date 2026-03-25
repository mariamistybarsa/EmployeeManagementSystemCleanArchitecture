using EmployeeManagement.Application.DTO.Request.Designations;
using EmployeeManagement.Application.Services.Interfaces.Designations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers.Designation;

[Route("api/[controller]")]
[ApiController]
public class DesignationController : ControllerBase
{
    private readonly IDesignationService _service;

    public DesignationController(IDesignationService service)
    {
        _service = service;
    }

    // 🔹 CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DesignationRequest request)
    {
        var response = await _service.AddAsync(request);
        return Ok(response);
    }

    // 🔹 GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var response = await _service.GetAllAsync(pageNumber, pageSize);
        return Ok(response);
    }

    // 🔹 GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _service.GetByIdAsync(id);
        return Ok(response);
    }

    // 🔹 UPDATE
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DesignationRequest request)
    {
        var response = await _service.UpdateAsync(id, request);
        return Ok(response);
    }

    // 🔹 DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _service.DeleteAsync(id);
        return Ok(response);
    }
}