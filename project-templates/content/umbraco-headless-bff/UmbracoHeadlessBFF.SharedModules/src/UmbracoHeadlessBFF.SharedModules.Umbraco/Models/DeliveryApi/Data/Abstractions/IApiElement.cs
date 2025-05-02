namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;

public interface IApiElement
{
    string Id { get; init; }
    string ContentType { get; }
}
