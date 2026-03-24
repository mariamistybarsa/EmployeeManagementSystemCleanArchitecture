// using EmployeeManagement.Application.DTO.Request;
// using EmployeeManagement.Application.Mappers;
//
// using EmployeeManagement.Persistence.UnitOfWork;
// using EmployeeManagement.Shared.DTO;
// using EmployeeManagement.Shared.Helpers;
//
//
// namespace EmployeeManagement.Application.Services.Implementations;
//
// public class DepartmentService : IDepartmentService
// {
//     private readonly IUnitOfWork _unitOfWork;
//
//     public DepartmentService(IUnitOfWork unitOfWork)
//     {
//         _unitOfWork = unitOfWork;
//     }
//
//     public async Task<StandardResponse> GetAllAsync()
//     {
//         var list = await _unitOfWork.Departments.GetAllAsync();
//
//         var data = list.Select(DepartmentMapper.ToResponse).ToList();
//
//         return ApiResponseHelper.Success( "Departments retrieved successfully");
//     }
//
//     public async Task<StandardResponse> CreateAsync(DepartmentRequest request)
//     {
//         var entity = DepartmentMapper.ToEntity(request);
//
//         await _unitOfWork.Departments.AddAsync(entity);
//         await _unitOfWork.SaveChangesAsync();
//
//         var data = DepartmentMapper.ToResponse(entity);
//
//         return ApiResponseHelper.Success("Department created successfully",data);
//     }
//
//     public async Task<StandardResponse> DeleteAsync(Guid id)
//     {
//         var entity = await _unitOfWork.Departments.GetByIdAsync(id);
//         if (entity == null)
//             return ApiResponseHelper.NotFound("Department not found");
//
//         _unitOfWork.Departments.Delete(entity);
//         await _unitOfWork.SaveChangesAsync();
//
//         return ApiResponseHelper.Success( "Department deleted successfully");
//     }
//
//     public async Task<StandardResponse> UpdateAsync(Guid id, DepartmentRequest request)
//     {
//         var entity = await _unitOfWork.Departments.GetByIdAsync(id);
//         if (entity == null)
//             return ApiResponseHelper.NotFound("Department not found");
//
//         entity.Name = request.Name;
//
//         _unitOfWork.Departments.Update(entity);
//         await _unitOfWork.SaveChangesAsync();
//
//         return ApiResponseHelper.Success(null, "Department updated successfully");
//     }
// }