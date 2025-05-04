namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Clients.Handlers;

public sealed class DeliveryApiHeadersHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Api-Key", ""); //TODO load api-key
        return base.SendAsync(request, cancellationToken);
    }
}
