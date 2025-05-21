using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

public sealed class SiteResolutionMiddleware : IMiddleware
{
    private readonly SiteResolutionService _siteResolutionService;
    private readonly SiteResolutionContext _siteResolutionContext;

    public SiteResolutionMiddleware(SiteResolutionService siteResolutionService, SiteResolutionContext siteResolutionContext)
    {
        _siteResolutionService = siteResolutionService;
        _siteResolutionContext = siteResolutionContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _ = context.Request.Query.TryGetValue("preview", out var preview);
        _ = bool.TryParse(preview, out var isPreview);

        _siteResolutionContext.IsPreview = isPreview;

        var site = await _siteResolutionService.ResolveSite();

        if (site is null)
        {
            await next(context);
            return;
        }

        _siteResolutionContext.SiteId = site.Value.SiteId;
        _siteResolutionContext.Site = site.Value.SiteDefinition;

        var hasSiteHost = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteHost, out var siteHost);
        if (hasSiteHost)
        {
            _siteResolutionContext.Domain = siteHost.ToString();
        }

        context.Response.OnStarting(() =>
        {
            if (context.Response.StatusCode > 299)
            {
                return Task.CompletedTask;
            }

            var headerExists = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteId, out _);

            if (!headerExists)
            {
                context.Response.Headers.Append(CorrelationConstants.Headers.SiteId, site.Value.SiteId);
            }

            return Task.CompletedTask;
        });

        await next(context);
    }
}
