using EmployeeManagement.Application.DTO.Request.Employees;
using EmployeeManagement.Application.DTO.Response.Employees;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.Employees;

public static class EmployeeMapper
{
    // 🔹 Create
    public static Employee MapRequestToEntity(EmployeeRequest request) => new()
    {
        Id = Guid.NewGuid(),
        DepartmentId = request.DepartmentId,
        DesignationId = request.DesignationId,

        FirstName = request.FirstName,
        LastName = request.LastName,
        Gender = request.Gender,

        JoinDate = request.JoinDate,
        DOB = request.DOB,

        PhoneNumber = request.PhoneNumber,
        Email = request.Email,
        Password = request.Password, // ⚠️ later hash

        PresentAddress = request.PresentAddress,
        EMP_Code = request.EmpCode,

        BaseSalary = request.BaseSalary,

        IsActive = true,
        IsDeleted = false,
        CreatedBy = Guid.Empty,
        CreatedDate = DateTime.UtcNow
    };

    // 🔹 Response
    public static EmployeeResponse MapEntityToResponse(Employee model) => new()
    { 
        Id= model.Id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber,
        EmpCode = model.EMP_Code,
        BaseSalary = model.BaseSalary
    };

   public static Employee MapRequestToUpdatedEntity(Employee oldEmployee, EmployeeUpdateRequest request)
   {
      
       if (request.DepartmentId.HasValue)
           oldEmployee.DepartmentId = request.DepartmentId.Value;
   
       if (request.DesignationId.HasValue)
           oldEmployee.DesignationId = request.DesignationId.Value;
   
       if (!string.IsNullOrWhiteSpace(request.FirstName))
           oldEmployee.FirstName = request.FirstName;
   
       if (!string.IsNullOrWhiteSpace(request.LastName))
           oldEmployee.LastName = request.LastName;
   
       if (!string.IsNullOrWhiteSpace(request.Gender))
           oldEmployee.Gender = request.Gender;
   
       if (request.JoinDate.HasValue)
           oldEmployee.JoinDate = request.JoinDate.Value;
   
       if (request.DOB.HasValue)
           oldEmployee.DOB = request.DOB.Value;
   
       if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
           oldEmployee.PhoneNumber = request.PhoneNumber;
   
       if (!string.IsNullOrWhiteSpace(request.Email))
           oldEmployee.Email = request.Email;
   
       if (!string.IsNullOrWhiteSpace(request.PresentAddress))
           oldEmployee.PresentAddress = request.PresentAddress;
   
       if (!string.IsNullOrWhiteSpace(request.EmpCode))
           oldEmployee.EMP_Code = request.EmpCode;
   
       if (request.BaseSalary.HasValue)
           oldEmployee.BaseSalary = request.BaseSalary.Value;
   
       oldEmployee.UpdatedBy = Guid.Empty;
   
       return oldEmployee;
   }

    public static List<EmployeeResponse> MapEntityListToResponseList(IEnumerable<Employee> models) =>
        models.Select(MapEntityToResponse).ToList();

    // 🔹 Clone
    public static Employee CloneEntity(Employee old) => new()
    {
        Id = old.Id,
        FirstName = old.FirstName,
        LastName = old.LastName,
        Email = old.Email,
        PhoneNumber = old.PhoneNumber,
        EMP_Code = old.EMP_Code,
        BaseSalary = old.BaseSalary,
        CreatedBy = old.CreatedBy,
        CreatedDate = old.CreatedDate
    };
}