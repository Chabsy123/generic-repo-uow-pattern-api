using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace generic_repo_uow_pattern_api.Exception
{
    public class TimeOutException : IExceptionHandler
    {
        private readonly ILogger<TimeOutException> _logger;

        public TimeOutException(ILogger<TimeOutException> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "A timeout occurred");

            if (exception is TimeoutException)
            {
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.RequestTimeout,
                    Type = exception.GetType().Name,
                    Title = "A timeout occurred",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                });

                return true;
            }
            return false;
        }
    }
}
