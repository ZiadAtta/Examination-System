using Examination_System.Common;

namespace Examination_System.Exceptions
{
    public class ValidationException:BaseAppException
    {
        public ValidationException(string message)
            : base(message, ErrorCode.ValidationError, StatusCodes.Status400BadRequest)
        {
        }

        public ValidationException(string message, ErrorCode errorCode)
            : base(message, errorCode, StatusCodes.Status400BadRequest)
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, ErrorCode.ValidationError, StatusCodes.Status400BadRequest, innerException)
        {
        }

        public ValidationException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status400BadRequest, innerException)
        {
        }
    }
}
