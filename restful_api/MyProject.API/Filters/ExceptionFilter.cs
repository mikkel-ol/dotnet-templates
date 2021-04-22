using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyProject.Domain.API;
using MyProject.Infrastructure.Exceptions;

namespace MyProject.API.Filters
{
    /// <summary>
    ///     Exception filter for the API.
    ///     Exceptions that should be processed and returned in a different format should be handled here.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        ///     Error result for returning errors with a message.
        /// </summary>
        private class ErrorResult : ObjectResult
        {
            public ErrorResult(int status, string type, string message) : base(new Error
            {
                Status = status,
                Type = type,
                Message = message
            })
            {
                StatusCode = status;
            }
        }
#pragma warning disable 1591
        private readonly IHostEnvironment environment;
        private readonly ILogger<ExceptionFilter> logger;

        public ExceptionFilter(IHostEnvironment environment, ILogger<ExceptionFilter> logger)
        {
            this.environment = environment;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException exception:
                    context.Result = new ErrorResult(StatusCodes.Status404NotFound, nameof(NotFoundException),
                        exception.Message);
                    break;

                case UnauthorizedAccessException exception:
                    context.Result = new ErrorResult(StatusCodes.Status401Unauthorized,
                        nameof(UnauthorizedAccessException),
                        exception.Message);
                    break;

                case ForbiddenException exception:
                    context.Result = new ErrorResult(StatusCodes.Status403Forbidden, nameof(ForbiddenException),
                        exception.Message);
                    break;

                case ValidationException exception:
                    var message = exception.Errors.Any()
                        ? new ValidationResult(exception.Errors).ToString()
                        : exception.Message;

                    context.Result = new ErrorResult(StatusCodes.Status400BadRequest, nameof(ValidationException),
                        message);
                    break;

                default:
                    if (environment.IsDevelopment()) return;

                    Console.Error.WriteLine(context.Exception.Message);
                    Console.Error.WriteLine(context.Exception.InnerException);

                    logger?.LogError("Internal server error: {@exception}", context.Exception);

                    context.Result = new ErrorResult(StatusCodes.Status500InternalServerError, nameof(Exception),
                        "Internal server error, contact an administrator");
                    break;
            }
        }
#pragma warning restore 1591
    }
}
