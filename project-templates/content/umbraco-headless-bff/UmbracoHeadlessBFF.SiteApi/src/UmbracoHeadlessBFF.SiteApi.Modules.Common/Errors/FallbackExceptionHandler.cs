using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

internal sealed class FallbackExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly IWebHostEnvironment _environment;

    public FallbackExceptionHandler(IProblemDetailsService problemDetailsService, IWebHostEnvironment environment)
    {
        _problemDetailsService = problemDetailsService;
        _environment = environment;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Title = "An error occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message,
            Extensions =
            {
                ["data"] = exception.Data
            }
        };

        if (!_environment.IsProd())
        {
            problemDetails.Extensions.Add("stackTrace", exception.StackTrace);
        }

        return await _problemDetailsService.TryWriteAsync(new()
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}
