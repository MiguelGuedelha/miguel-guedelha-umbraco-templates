using Microsoft.AspNetCore.Http;

namespace UmbracoBFFAstro.SharedModules.Features.Correlation;

public class CorrelationIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request.Headers["x-correlation-id"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Response.Headers.Append("x-correlation-id", correlationId);
        }

        await next(context);
    }
}
