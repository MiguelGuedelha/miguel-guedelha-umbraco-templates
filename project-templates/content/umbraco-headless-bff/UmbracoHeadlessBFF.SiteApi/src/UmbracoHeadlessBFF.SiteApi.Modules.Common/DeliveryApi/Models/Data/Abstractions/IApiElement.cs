namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;

public interface IApiElement
{
    string Id { get; init; }
    string ContentType { get; }
}
