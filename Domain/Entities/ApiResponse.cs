
namespace Domain.Entities
{
    public class ApiResponse<T>(bool success, T? data, string? message)
    {
        public bool Success { get; set; } = success;
        public T? Data { get; set; } = data;
        public string? Message { get; set; } = message;
    }
}
