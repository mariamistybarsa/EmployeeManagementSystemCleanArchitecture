using EmployeeManagement.Shared.DTO.Response;

namespace EmployeeManagement.Shared.DTO;

public class StandardResponse
{
   
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public List<ErrorDetails>? Errors { get; set; }
    
}