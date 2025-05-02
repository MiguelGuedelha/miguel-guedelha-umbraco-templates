using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;

public interface IApiRteDescription
{
    ApiRichTextItem? Description { get; init; }
}
