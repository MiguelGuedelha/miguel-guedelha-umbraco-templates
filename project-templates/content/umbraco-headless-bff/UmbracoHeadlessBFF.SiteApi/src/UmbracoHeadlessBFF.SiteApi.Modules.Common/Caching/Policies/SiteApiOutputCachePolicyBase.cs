using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public abstract class SiteApiOutputCachePolicyBase
{
    protected static bool CanCacheBase(OutputCacheContext context, SiteResolutionContext siteResolutionContext, [NotNullWhen(true)] out string? siteId)
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

        try
        {
            siteId = siteResolutionContext.SiteId;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected static void ServeResponseBaseAsync(OutputCacheContext context)
    {
        var response = context.HttpContext.Response;

        // Verify existence of cookie headers
        if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
        {
            context.AllowCacheStorage = false;
            return;
        }

        // Check response code
        if (response.StatusCode is StatusCodes.Status200OK)
        {
            return;
        }

        context.AllowCacheStorage = false;
    }
}
