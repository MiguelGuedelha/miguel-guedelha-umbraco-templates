using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

internal sealed class RedirectApiExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not RedirectApiException redirectApiException)
        {
            return ValueTask.FromResult(false);
        }

        httpContext.Response.Redirect(redirectApiException.Location, redirectApiException.IsPermanent);

        return ValueTask.FromResult(true);
    }
}
