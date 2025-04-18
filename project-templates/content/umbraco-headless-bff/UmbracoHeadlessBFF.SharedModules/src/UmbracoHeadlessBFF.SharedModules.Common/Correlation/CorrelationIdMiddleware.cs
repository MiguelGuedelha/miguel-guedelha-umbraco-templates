using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common.Constants;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

internal class CorrelationIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request.Headers[SharedConstants.Common.Correlation.Headers.CorrelationId].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Response.Headers.Append(SharedConstants.Common.Correlation.Headers.CorrelationId, correlationId);
        }

        await next(context);
    }
}
