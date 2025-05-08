using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Preview.Clients;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Preview;

public class PreviewMiddleware : IMiddleware
{
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly IPreviewVerificationApi _previewVerificationApi;

    public PreviewMiddleware(SiteResolutionContext siteResolutionContext, IPreviewVerificationApi previewVerificationApi)
    {
        _siteResolutionContext = siteResolutionContext;
        _previewVerificationApi = previewVerificationApi;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!_siteResolutionContext.IsPreview)
        {
            await next(context);
            return;
        }

        var exists = context.Request.Query.TryGetValue("previewToken", out var previewToken);

        if (!exists)
        {
            throw new SiteApiException(StatusCodes.Status401Unauthorized, "No preview token found");
        }

        var token = previewToken.ToString();

        var response = await _previewVerificationApi.VerifyPreviewMode($"Bearer {token}");

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException(StatusCodes.Status401Unauthorized, "Invalid preview token");
        }

        await next(context);
    }
}
