using NoteTakingApi.Common.Models;
using System;

namespace NoteTakingApi.Common.Exceptions
{
    public class BaseException : Exception
    {
        public ErrorResponse ErrorResponse { get; }
        public BaseException(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
