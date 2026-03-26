using EmployeeManagement.Application.DTO.Request.Auth;
using EmployeeManagement.Application.Mappers.Auth;
using EmployeeManagement.Application.Services.Interfaces.Auth;
using EmployeeManagement.Application.Services.Interfaces.JWTtoken;
using EmployeeManagement.Application.Validators.Auth;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.Repositories.Interfaces.Users;
using EmployeeManagement.Persistence.UnitOfWork;
using EmployeeManagement.Shared.DTO;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Shared.Helpers;
using FluentValidation;

namespace EmployeeManagement.Application.Services.Implementations.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RegisterRequest> _validator;
    private readonly IValidator<LoginRequest> _LoginRequestValidator;
    private readonly IJWTtokenService _jwttokenService;

    public AuthService(
        IUnitOfWork unitOfWork,
        IValidator<RegisterRequest> validator , IValidator<LoginRequest> LoginRequestValidator, IJWTtokenService jwttokenService)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _LoginRequestValidator = LoginRequestValidator;
        _jwttokenService=jwttokenService;
    }

    public async Task<StandardResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var validationResult = await _LoginRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                    .ToList();

                return ApiResponseHelper.ValidationError("Login", errors);
            }

            var existvalue = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (existvalue == null)
            {
                return ApiResponseHelper.ValidationError("Email Not Availabe", new List<ErrorDetails>());
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existvalue.Password))
            {
                return ApiResponseHelper.FailedToCreate("Password Is not Match",new List<ErrorDetails>());
            }

            var employee = await _unitOfWork.Employees.GetByIdAsync(existvalue.EmployeeId);
            var jwttoken= await _jwttokenService.GenerateToken(existvalue, employee);
            var response = LoginMapper.ToLoginResponse(existvalue,jwttoken.AccessToken);
               return ApiResponseHelper.Success("Login", response);
            
        }

        catch (Exception e)
        {
            return ApiResponseHelper.FailedToCreate(e.Message, new List<ErrorDetails>());
        }
        
    }

    public async Task<StandardResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new ErrorDetails(e.PropertyName, e.ErrorMessage))
                    .ToList();

                return ApiResponseHelper.ValidationError("Register", errors);
            }

            var existvalue = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (existvalue != null)
            {
                return ApiResponseHelper.ValidationError("Email already exists", new List<ErrorDetails>());
            }

            var value = AuthMapper.ToEmployee(request);
            var employee = await _unitOfWork.Employees.AddAsync(value);
            await _unitOfWork.SaveChangesAsync();

            var data = AuthMapper.ToUser(request, employee.Id);
            await _unitOfWork.Users.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            var response = AuthMapper.MapToResponse(request);
            return ApiResponseHelper.Success("Register", response);


        }
        catch (Exception e)
        {
            return ApiResponseHelper.FailedToCreate(e.Message, new List<ErrorDetails>());
        }

    }
}


