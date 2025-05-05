using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Handlers;

public class CmsApiKeyHeaderHandler : DelegatingHandler
{
    private readonly IOptionsMonitor<CmsServiceOptions> _cmsServiceOptions;

    public CmsApiKeyHeaderHandler(IOptionsMonitor<CmsServiceOptions> cmsServiceOptions)
    {
        _cmsServiceOptions = cmsServiceOptions;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKey = _cmsServiceOptions.CurrentValue.Parameters.DeliveryApiKey;
        request.Headers.Add("x-api-key", apiKey);
        return base.SendAsync(request, cancellationToken);
    }
}
