using Examination_System.Common;

namespace Examination_System.Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException(string message, ErrorCode errorCode)
            : base(message, errorCode, StatusCodes.Status404NotFound)
        {
        }

        public NotFoundException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status404NotFound, innerException)
        {                                                
        }
    }
}                                  
