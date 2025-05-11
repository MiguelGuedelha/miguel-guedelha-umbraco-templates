using Microsoft.AspNetCore.Http;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

internal sealed class CorrelationIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request.Headers[CorrelationConstants.Headers.CorrelationId].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers.Append(CorrelationConstants.Headers.CorrelationId, correlationId);
            context.Response.Headers.Append(CorrelationConstants.Headers.CorrelationId, correlationId);
        }

        await next(context);
    }
}
