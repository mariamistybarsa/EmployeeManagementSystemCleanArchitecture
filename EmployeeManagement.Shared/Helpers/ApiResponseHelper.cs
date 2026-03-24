using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;
using StatusCodes = EmployeeManagement.Shared.DTO.StatusCodes;

namespace EmployeeManagement.Shared.Helpers;


public static class ApiResponseHelper
{
    public static StandardResponse Success(string entityName, object? data = null) =>
        new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Success200,
            Message = $"{entityName}",
            Data = data
        };

    public static StandardResponse Created(string entityName, object? data = null, string? customMassage = null) =>
        new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Created201,
            Message = customMassage ?? $"{entityName} created successfully.",
            Data = data
        };

    public static StandardResponse Updated(string entityName, object? data = null, string? customMassage = null) =>
        new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Success200,
            Message = customMassage ?? $"{entityName} updated successfully.",
            Data = data
        };

    public static StandardResponse Deleted(string entityName, object? data = null) =>
        new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Success200,
            Message = $"{entityName} deleted successfully.",
            Data = data
        };

    public static StandardResponse NotFound(string entityName, object? data = null) =>
        new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.NotFound404,
            Message = $"{entityName} not found.",
            Data = data
        };

    public static StandardResponse ValidationError(string massage, List<ErrorDetails> errorList) =>
        new()
        {
            IsSuccess = false,
            StatusCode = StatusCodes.UnprocessableEntity422,
            Message = massage ?? (errorList.FirstOrDefault()?.Message ?? string.Empty),
            Errors = errorList
        };
    
    public static StandardResponse FailedToCreate(string entityName, List<ErrorDetails> errorList) =>
        new()
        {
            IsSuccess = false,
            StatusCode = StatusCodes.ServerError500,
            Message = $"{entityName} could not be created at the moment. Please verify the information and try again.",
            Errors = errorList
        };
    
    public static StandardResponse
        FailedToUpdate(string entityName, List<ErrorDetails> errorList, string errorMassage) =>
        new()
        {
            IsSuccess = false,
            StatusCode = StatusCodes.UnprocessableEntity422,
            Message = errorMassage ?? string.Empty,
            Errors = errorList
        };
    
    public static StandardResponse Forbidden(string entityName, string? customMessage = null, object? data = null) =>
        new()
        {
            IsSuccess = false,
            StatusCode = StatusCodes.Forbidden403,
            Message = customMessage ?? $"{entityName} access is forbidden.",
            Data = data
        };
    
    public static StandardResponse Unauthorized(string entity, string? message = null, object? data = null)
        => new()
        {
            IsSuccess = false,
            StatusCode = StatusCodes.Unauthorized401,
            Message = message ?? $"{entity}: You are not authorized.",
            Data = data
        };
}