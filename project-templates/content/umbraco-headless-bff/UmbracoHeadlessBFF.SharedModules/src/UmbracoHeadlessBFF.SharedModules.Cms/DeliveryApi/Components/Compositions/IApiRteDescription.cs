using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components.Compositions;

public interface IApiRteDescription
{
    ApiRichTextItem? Description { get; init; }
}
