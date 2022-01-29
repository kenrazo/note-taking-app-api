using System.Collections.Generic;

namespace NoteTakingApi.Common.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(string message, string referenceKey = null)
        {
            Message = message;
            ReferenceKey = referenceKey;
        }

        public string Message { get; set; }
        public string ReferenceKey { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
