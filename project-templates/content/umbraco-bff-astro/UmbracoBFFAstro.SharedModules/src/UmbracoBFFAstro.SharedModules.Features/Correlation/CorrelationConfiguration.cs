using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoBFFAstro.SharedModules.Features.Correlation;

public static class CorrelationConfiguration
{
    /// <summary>
    /// Configures default correlation settings (including propagation of the correlation header)
    /// Requires that each configured Http Client has .AddHeaderPropagation() added to it to full work end-to-end
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to be configured</param>
    public static void AddCorrelation(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<CorrelationIdMiddleware>();
        builder.Services.AddHeaderPropagation(o =>
        {
            o.Headers.Add("x-correlation-id");
        });
    }

    public static void UseCorrelation(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseHeaderPropagation();
    }
}
