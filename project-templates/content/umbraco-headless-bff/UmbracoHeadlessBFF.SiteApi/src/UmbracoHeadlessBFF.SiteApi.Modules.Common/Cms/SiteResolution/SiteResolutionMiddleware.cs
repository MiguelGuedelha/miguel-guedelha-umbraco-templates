using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common;

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

        _siteResolutionContext.SiteId = site.Value.Item1;
        _siteResolutionContext.Site = site.Value.Item2;

        context.Response.OnStarting(() =>
        {
            if (context.Response.StatusCode > 299)
            {
                return Task.CompletedTask;
            }

            var headerExists = context.Request.Headers.TryGetValue(SharedConstants.Common.Correlation.Headers.SiteId, out _);

            if (!headerExists)
            {
                context.Response.Headers.Append(SharedConstants.Common.Correlation.Headers.SiteId, site.Value.SiteId);
            }

            return Task.CompletedTask;
        });
    }
}
