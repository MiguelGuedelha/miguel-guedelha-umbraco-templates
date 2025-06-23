using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public abstract class SiteApiOutputCachePolicyBase
{
    protected static bool CanCacheBySite(OutputCacheContext context, [NotNullWhen(true)] out string? siteId)
    {
        var request = context.HttpContext.Request;

        siteId = null;

        if (!HttpMethods.IsGet(request.Method)
            && !HttpMethods.IsHead(request.Method)
            && !HttpMethods.IsPost(request.Method))
        {
            return false;
        }

        if (!StringValues.IsNullOrEmpty(request.Headers.Authorization) ||
            request.HttpContext.User is { Identity.IsAuthenticated: true })
        {
            return false;
        }

        var siteContext = context.HttpContext.RequestServices.GetService<SiteResolutionContext>();

        if (siteContext is null)
        {
            return false;
        }

        try
        {
            siteId = siteContext.SiteId;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
