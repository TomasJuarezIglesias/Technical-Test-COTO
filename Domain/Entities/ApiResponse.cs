
namespace Domain.Entities
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        private ApiResponse(bool success, T? data, string? message = ""){
            this.Success = success; 
            this.Data = data; 
            this.Message = message;
        }

        public static ApiResponse<T> SuccessResponse(T? data, string? message = null)
        {
            return new ApiResponse<T>(true, data, message);
        }

        public static ApiResponse<T> ErrorResponse(string? message = null)
        {
            return new ApiResponse<T>(true, default, message);
        }

    }
}
