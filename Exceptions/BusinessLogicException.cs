using Examination_System.Common;

namespace Examination_System.Exceptions
{
    public class BusinessLogicException : BaseAppException
    {
        public BusinessLogicException(string message, ErrorCode errorCode)
           : base(message, errorCode, StatusCodes.Status400BadRequest)
        {
        }

        public BusinessLogicException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status400BadRequest, innerException)
        {
        }
    }
}
