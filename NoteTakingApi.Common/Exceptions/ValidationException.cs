using NoteTakingApi.Common.Models;

namespace NoteTakingApi.Common.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(ErrorResponse errorResponse) : base(errorResponse)
        {
        }
    }
}
