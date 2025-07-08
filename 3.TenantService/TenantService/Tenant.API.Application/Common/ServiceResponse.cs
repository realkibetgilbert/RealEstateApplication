namespace Tenant.API.Application.Common
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ServiceResponse<T> Success(T data)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = true,
                Title = "Success",
                Message = "Request completed successfully.",
                Data = data
            };
        }

        public static ServiceResponse<T> Failure()
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                Title = "Fail",
                Message = "Request Failed",
                Data = default
            };
        }
    }
}
