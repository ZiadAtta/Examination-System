using Examination_System.Common;
using Microsoft.Identity.Client;
using System.Net;

namespace Examination_System.Exceptions
{
    public class BaseAppException:Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; } = 500;
        public string message { get; set; } = "An unexpected error occurred.";
        public BaseAppException():base() { }
        public BaseAppException(string message) : base(message) { }
        public BaseAppException(string message, Exception innerExceptoin) : base(message, innerExceptoin) { }
        public BaseAppException(string message, ErrorCode errorCode) : base(message) { }
        public BaseAppException(string message, ErrorCode errorCode, int statusCode) : base(message) { }
        public BaseAppException(string message, ErrorCode errorCode, int statusCode, Exception innerException) 
            : base(message, innerException) 
        {
        }
    }
}
