using Microsoft.AspNetCore.Http;
using Models.Consts;
using Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Services.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly Dictionary<string, HttpStatusCode> ErrorCodesMap = new Dictionary<string, HttpStatusCode>
        {
            { ErrorCodes.NotFound, HttpStatusCode.NotFound },
            { ErrorCodes.PermissionError, HttpStatusCode.Forbidden },
            { ErrorCodes.AccountBlocked, HttpStatusCode.Forbidden },
            { ErrorCodes.IncorrectCreds, HttpStatusCode.BadRequest },
            { ErrorCodes.Unknown, HttpStatusCode.InternalServerError },
            { ErrorCodes.Conflict, HttpStatusCode.BadRequest },
            { ErrorCodes.BadRequest, HttpStatusCode.BadRequest },
            { ErrorCodes.RequestFailed, HttpStatusCode.FailedDependency }
        };

        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string error = exception.Message;
            string errorCode = ErrorCodes.Unknown;
            string stackTrace = exception.StackTrace;
            object errorModel = null;
            var httpStatusCode = HttpStatusCode.InternalServerError;

            if (exception is BaseException baseException)
            {
                errorCode = baseException.ErrorCode;
                httpStatusCode = ErrorCodesMap[errorCode];
                stackTrace = null;
                errorModel = new ErrorModel()
                {
                    Error = error,
                    ErrorCode = errorCode,
                    StackTrace = stackTrace
                };
            }

            else
            {
                errorModel = new ErrorModel()
                {
                    Error = error,
                    ErrorCode = errorCode,
                    StackTrace = stackTrace
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorModel));
        }
    }
}
