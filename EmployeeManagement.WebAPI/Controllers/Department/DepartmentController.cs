using System;
using System.Threading.Tasks;
using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Application.DTO.Request.Department;
using EmployeeManagement.Application.Services.Interfaces;
using EmployeeManagement.Application.Services.Interfaces.Department;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers.Department;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        return Ok(await _service.GetAllAsync(pageNumber,pageSize));
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentRequest request)
    {
        return Ok(await _service.AddAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, DepartmentRequest request)
    {
        return Ok(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}