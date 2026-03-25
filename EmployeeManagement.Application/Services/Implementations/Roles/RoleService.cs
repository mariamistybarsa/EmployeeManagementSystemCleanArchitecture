using EmployeeManagement.Application.DTO.Request.Roles;
using EmployeeManagement.Application.Mappers.Roles;
using EmployeeManagement.Application.Services.Interfaces.Roles;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;

namespace EmployeeManagement.Application.Services.Implementations.Roles;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StandardResponse> AddAsync(RoleRequest request)
    {
        var existingRoles = await _unitOfWork.Roles.GetAllAsync();

        if (existingRoles.Any(x => !x.IsDeleted && x.Name == request.Name))
        {
            return ApiResponseHelper.ValidationError("Role",
                new List<ErrorDetails> { new("Name", "Role already exists") });
        }

        var entity = RoleMapper.ToEntity(request);

        await _unitOfWork.Roles.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var data = RoleMapper.ToResponse(entity);

        return ApiResponseHelper.Success("Role created successfully", data);
    }

    public Task<StandardResponse> UpdateAsync(Guid id, RoleRequest entity)
    {
        throw new NotImplementedException();
    }

   
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.Roles.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(RoleMapper.ToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No roles found");

        return ApiResponseHelper.Success("Roles retrieved successfully", data);
    }

 
    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.Roles.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Role not found");

        var data = RoleMapper.ToResponse(entity);

        return ApiResponseHelper.Success("Role retrieved successfully", data);
    }

   
    public async Task<StandardResponse> UpdateRoleAsync(Guid id, RoleUpdateRequest request)
    {
        var entity = await _unitOfWork.Roles.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Role not found");

        RoleMapper.MapRequestToUpdatedEntity(entity, request);

        _unitOfWork.Roles.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Role updated successfully");
    }

 
    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Roles.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("Role not found");

        entity.IsDeleted = true;

        _unitOfWork.Roles.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("Role deleted successfully");
    }
}