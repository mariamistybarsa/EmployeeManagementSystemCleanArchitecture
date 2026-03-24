// using EmployeeManagement.Application.DTO.Request;
// using EmployeeManagement.Application.DTO.Response;
// using EmployeeManagement.Domain.Models;
//
// namespace EmployeeManagement.Application.Mappers;
//
// public static class DepartmentMapper
// {
//     public static Department ToEntity(DepartmentRequest request)
//     {
//         return new Department
//         {
//             Id = Guid.NewGuid(),
//             Name = request.Name
//         };
//     }
//
//     public static DepartmentResponse ToResponse(Department entity)
//     {
//         return new DepartmentResponse
//         {
//             Id = entity.Id,
//             Name = entity.Name
//         };
//     }
// }