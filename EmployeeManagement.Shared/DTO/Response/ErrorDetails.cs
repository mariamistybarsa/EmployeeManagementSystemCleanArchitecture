namespace EmployeeManagement.Shared.DTO.Response;
public class ErrorDetails(string field, string message)
{
    public string Field { get; set; } = field;
    public string Message { get; set; } = message;
}