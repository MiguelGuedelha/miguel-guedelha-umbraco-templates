using Microsoft.Extensions.Logging;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Layouts;

internal sealed class FallbackLayoutMapper : ILayoutMapper
{
    private readonly ILogger<FallbackLayoutMapper> _logger;

    public FallbackLayoutMapper(ILogger<FallbackLayoutMapper> logger)
    {
        _logger = logger;
    }

    public bool CanMap(string type) => true;

    public Task<ILayout?> Map(IApiBlockGridItem model)
    {
        _logger.LogWarning("Fallback: Layout {Id} of type {ContentType}", model.Content.Id, model.Content.ContentType);

        return Task.FromResult<ILayout?>(new FallbackLayout
        {
            Id = model.Content.Id,
            ContentType = model.Content.ContentType,
        });
    }
}
