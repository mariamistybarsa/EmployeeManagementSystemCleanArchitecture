using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Application.DTO.Request.Users;
using EmployeeManagement.Application.Mappers;
using EmployeeManagement.Application.Mappers.Users;
using EmployeeManagement.Application.Services.Interfaces.Users;
using EmployeeManagement.Application.Validators.Common;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;

namespace EmployeeManagement.Application.Services.Implementations.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UserRequest> _validator;
    private readonly IValidator<UserUpdateRequest> _updatevalidator;

    public UserService(IUnitOfWork unitOfWork, IValidator<UserRequest> validator, IValidator<UserUpdateRequest> updatevalidator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _updatevalidator = updatevalidator;
    }

    public async Task<StandardResponse> AddAsync(UserRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        var errors = validationResult.Errors
            .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
            .ToList();

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors.Add(new ErrorDetails("Email", "Email is required"));
        }

        var emailErrors = await CommonValidator.ValidateEmailAsync(request.Email);
        errors.AddRange(emailErrors);

        var passwordErrors = await CommonValidator.ValidatePasswordAsync(request.Password);
        errors.AddRange(passwordErrors);

        var existingUsers = await _unitOfWork.Users.GetAllAsync();

        if (existingUsers.Any(x => !x.IsDeleted && x.Email == request.Email))
        {
            errors.Add(new ErrorDetails("Email", "Email already exists"));
        }

        if (errors.Any())
            return ApiResponseHelper.ValidationError("User", errors);

        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
            return ApiResponseHelper.NotFound("Role not found");

        var employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
        if (employee == null)
            return ApiResponseHelper.NotFound("Employee not found");

        var entity = UserMapper.ToEntity(request);

        await _unitOfWork.Users.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var data = UserMapper.ToResponse(entity);

        return ApiResponseHelper.Success("User created successfully", data);
    }

    public Task<StandardResponse> UpdateAsync(Guid id, UserRequest entity)
    {
        throw new NotImplementedException();
    }

    // 🔹 GET ALL
    public async Task<StandardResponse> GetAllAsync(int? pageNumber, int? pageSize)
    {
        var list = await _unitOfWork.Users.GetAllAsync();

        var data = list
            .Where(x => !x.IsDeleted)
            .Select(UserMapper.ToResponse)
            .ToList();

        if (!data.Any())
            return ApiResponseHelper.NotFound("No users found");

        return ApiResponseHelper.Success("Users retrieved successfully", data);
    }

    // 🔹 GET BY ID
    public async Task<StandardResponse> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("User not found");

        var data = UserMapper.ToResponse(entity);

        return ApiResponseHelper.Success("User retrieved successfully", data);
    }

    // 🔹 UPDATE
    public async Task<StandardResponse> UpdateUserAsync(Guid id, UserUpdateRequest request)
    {
        var validationResult = await _updatevalidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                .ToList();

            return ApiResponseHelper.ValidationError("User", errors);
        }

        var entity = await _unitOfWork.Users.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("User not found");

        UserMapper.MapRequestToUpdatedEntity(entity, request);

        _unitOfWork.Users.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("User updated successfully");
    }

    // 🔹 DELETE (Soft Delete)
    public async Task<StandardResponse> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Users.GetByIdAsync(id);

        if (entity == null || entity.IsDeleted)
            return ApiResponseHelper.NotFound("User not found");

        entity.IsDeleted = true;

        _unitOfWork.Users.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseHelper.Success("User deleted successfully");
    }
}