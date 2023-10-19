using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using PaintyTestContext.Application.Common.Exceptions;
using PaintyTestContext.Models;

namespace PaintyTestContext.Middlewares.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            int statusCode = 0;
            var message = string.Empty;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                case ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ForbiddenException:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new ErrorModel
            {
                StatusCode = statusCode,
                Message = message == string.Empty ? exception.Message : message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
