using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;

public interface IApiRteDescription
{
    ApiRichTextItem? Description { get; init; }
}
