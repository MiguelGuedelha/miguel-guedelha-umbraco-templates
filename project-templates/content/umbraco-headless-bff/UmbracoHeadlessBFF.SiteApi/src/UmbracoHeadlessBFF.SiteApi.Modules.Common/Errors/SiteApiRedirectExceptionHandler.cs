using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

internal sealed class SiteApiRedirectExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var redirectApiException = exception as SiteApiRedirectException;

        if (redirectApiException is null && exception is FusionCacheFactoryException factoryException)
        {
            redirectApiException = factoryException.InnerException as SiteApiRedirectException;
        }

        if (redirectApiException is null)
        {
            return ValueTask.FromResult(false);
        }

        httpContext.Response.Redirect(redirectApiException.Location, redirectApiException.IsPermanent);

        return ValueTask.FromResult(true);
    }
}
