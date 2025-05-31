using Microsoft.Extensions.Logging;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class FallbackComponentMapper : IComponentMapper
{
    private readonly ILogger<FallbackComponentMapper> _logger;

    public FallbackComponentMapper(ILogger<FallbackComponentMapper> logger)
    {
        _logger = logger;
    }

    public bool CanMap(string type) => true;

    public Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        _logger.LogWarning("Fallback: Component {Id} of type {ContentType}", model.Id, model.ContentType);

        return Task.FromResult<IComponent?>(new FallbackComponent
        {
            Id = model.Id,
            ContentType = model.ContentType,
        });
    }
}
