namespace EmployeeManagement.Application.DTO.Response.Auth;

public class TokenCreationResponse
{
    public string AccessToken { get; set; }
    public DateTime ExpiresAtutc { get; set; }
}