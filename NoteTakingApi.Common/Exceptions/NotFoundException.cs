using NoteTakingApi.Common.Models;

namespace NoteTakingApi.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
        {
        }
    }
}
