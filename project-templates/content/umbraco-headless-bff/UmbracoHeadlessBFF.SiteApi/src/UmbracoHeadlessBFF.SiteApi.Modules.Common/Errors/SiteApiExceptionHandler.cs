using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

internal sealed class SiteApiExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly IWebHostEnvironment _environment;

    public SiteApiExceptionHandler(IProblemDetailsService problemDetailsService, IWebHostEnvironment environment)
    {
        _problemDetailsService = problemDetailsService;
        _environment = environment;
    }


    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not SiteApiException siteApiException)
        {
            return false;
        }

        var statusCode = siteApiException.StatusCode;
        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
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
