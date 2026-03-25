using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Application.DTO.Request.Users;
using EmployeeManagement.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int? pageNumber, int? pageSize)
        => Ok(await _service.GetAllAsync(pageNumber, pageSize));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request)
        => Ok(await _service.AddAsync(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UserUpdateRequest request)
        => Ok(await _service.UpdateUserAsync(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _service.DeleteAsync(id));
}