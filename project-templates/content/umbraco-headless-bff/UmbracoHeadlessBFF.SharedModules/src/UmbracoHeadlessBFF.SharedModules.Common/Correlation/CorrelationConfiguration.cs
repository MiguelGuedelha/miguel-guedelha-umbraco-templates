using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public static class CorrelationConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCorrelationCommonSharedModule()
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
    }

    extension(WebApplication app)
    {
        public void UseCorrelationSharedModules()
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseHeaderPropagation();
        }
    }
}
