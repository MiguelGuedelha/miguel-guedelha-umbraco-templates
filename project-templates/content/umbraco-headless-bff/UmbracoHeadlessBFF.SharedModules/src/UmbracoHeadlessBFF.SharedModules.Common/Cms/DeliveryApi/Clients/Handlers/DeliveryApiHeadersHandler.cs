using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients.Handlers;

public sealed class DeliveryApiHeadersHandler : DelegatingHandler
{
    private readonly IOptionsMonitor<CmsServiceOptions> _cmsServiceOptions;

    public DeliveryApiHeadersHandler(IOptionsMonitor<CmsServiceOptions> cmsServiceOptions)
    {
        _cmsServiceOptions = cmsServiceOptions;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKey = _cmsServiceOptions.CurrentValue.Parameters.DeliveryApiKey;
        request.Headers.Add("Api-Key", apiKey);
        return base.SendAsync(request, cancellationToken);
    }
}
