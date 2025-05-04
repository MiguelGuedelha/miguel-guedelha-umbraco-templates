using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Abstractions;

public interface IApiRteDescription
{
    ApiRichTextItem? Description { get; init; }
}
