using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

public static class ErrorsConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddErrorsCommonModule()
        {
            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                    context.ProblemDetails.Extensions.TryAdd("correlationId", context.HttpContext.Request.Headers[CorrelationConstants.Headers.CorrelationId].ToString());
                };
            });
            builder.Services.AddExceptionHandler<SiteApiRedirectExceptionHandler>();
            builder.Services.AddExceptionHandler<SiteApiExceptionHandler>();
            builder.Services.AddExceptionHandler<FallbackExceptionHandler>();
        }
    }
}
