using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Application.DTO.Request.Department;
using EmployeeManagement.Application.Services.Interfaces.Generic;

namespace EmployeeManagement.Application.Services.Interfaces.Department;

public interface IDepartmentService : IGenericService<DepartmentRequest>
{
}