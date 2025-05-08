using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public static class CorrelationConfiguration
{
    public static void AddCorrelationSharedModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<CorrelationIdMiddleware>();
        builder.Services.AddHeaderPropagation(options =>
        {
            options.Headers.Add(SharedConstants.Common.Correlation.Headers.CorrelationId);
            options.Headers.Add(SharedConstants.Common.Correlation.Headers.SiteHost);
            options.Headers.Add(SharedConstants.Common.Correlation.Headers.SitePath);
        });
    }

    public static void UseCorrelationSharedModules(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseHeaderPropagation();
    }
}
