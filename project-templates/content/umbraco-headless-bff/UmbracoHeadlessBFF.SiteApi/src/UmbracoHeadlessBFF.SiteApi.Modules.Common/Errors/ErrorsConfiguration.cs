using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

public static class ErrorsConfiguration
{
    public static void AddErrors(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                context.ProblemDetails.Extensions.TryAdd("correlationId", context.HttpContext.Request.Headers[CorrelationConstants.Headers.CorrelationId].ToString());
            };
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    }

    public static void UseErrors(this WebApplication app)
    {
        app.UseExceptionHandler();
    }
}
