using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public static class CorrelationConfiguration
{
    public static void AddCorrelationCommonSharedModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<CorrelationIdMiddleware>();
        builder.Services.AddHeaderPropagation(options =>
        {
            options.Headers.Add(CorrelationConstants.Headers.CorrelationId);
            options.Headers.Add(CorrelationConstants.Headers.SiteHost);
            options.Headers.Add(CorrelationConstants.Headers.SitePath);
        });
    }

    public static void UseCorrelationSharedModules(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseHeaderPropagation();
    }
}
