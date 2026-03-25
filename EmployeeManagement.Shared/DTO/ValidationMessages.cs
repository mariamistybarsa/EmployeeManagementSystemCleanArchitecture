namespace EmployeeManagement.Shared.DTO;

public class ValidationMessages
{
    #region Email Validation
    public const string EmailAlreadyExists = "Email already exists.";
    public const string EmailRequired = "Email is required.";
    public const string InvalidEmailFormat = "A valid email is required.";
    #endregion

    #region Password Validation
    public const string PasswordRequired = "Password is required.";
    public const string PasswordMinLength = "Password must be at least 8 characters long.";
    public const string PasswordUppercase = "Password must contain at least one uppercase letter.";
    public const string PasswordLowercase = "Password must contain at least one lowercase letter.";
    public const string PasswordDigit = "Password must contain at least one number.";
    public const string PasswordOldInvalid = "Old Password is invalid.";
    #endregion
}