using NoteTakingApi.Common.Models;

namespace NoteTakingApi.Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
        {
        }
    }
}
