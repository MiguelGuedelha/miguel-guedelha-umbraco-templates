using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UmbracoHeadlessBFF.SharedModules.Cms.Preview;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Preview;

public sealed class PreviewMiddleware : IMiddleware
{
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly IPreviewVerificationApi _previewVerificationApi;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<PreviewMiddleware> _logger;

    public PreviewMiddleware(SiteResolutionContext siteResolutionContext, IPreviewVerificationApi previewVerificationApi,
        IWebHostEnvironment environment, ILogger<PreviewMiddleware> logger)
    {
        _siteResolutionContext = siteResolutionContext;
        _previewVerificationApi = previewVerificationApi;
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!_siteResolutionContext.IsPreview)
        {
            await next(context);
            return;
        }

        var token = _siteResolutionContext.PreviewToken;

        if (string.IsNullOrWhiteSpace(token))
        {
            if (!_environment.IsLocal())
            {
                throw new SiteApiException(StatusCodes.Status401Unauthorized, "No preview token found");
            }

            _logger.LogInformation("No preview token found, but is local environment, will allow through");
            await next(context);
            return;
        }

        var response = await _previewVerificationApi.VerifyPreviewMode($"Bearer {token}");

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException(StatusCodes.Status401Unauthorized, "Invalid preview token");
        }

        await next(context);
    }
}
