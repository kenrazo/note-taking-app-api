using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NoteTakingApi.Common.Exceptions;
using NoteTakingApi.Common.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NoteTakingApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, BaseException ex)
        {
            var code = HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case ValidationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            var result = string.Empty;
            if (!string.IsNullOrEmpty(ex.Message))
            {
                result = JsonConvert.SerializeObject(ex.ErrorResponse);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorReferenceKey = Guid.NewGuid().ToString();
            var result = new ErrorResponse
            { Message = "A unhandled exception has occurred", ReferenceKey = errorReferenceKey };
            var resultString = JsonConvert.SerializeObject(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(resultString);
        }
    }
}
