namespace Examination_System.Common
{
    public class GeneralResponse<T> 
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }

        public static GeneralResponse<T> Response(T? data = default, string Message = "",bool isSuccess = false,string? ErrorCode = null)
        {
            return new GeneralResponse<T>
            {
                IsSuccess = isSuccess,
                Message = Message,
                Data = data,
                ErrorCode = ErrorCode
            };
        }
    }
}
