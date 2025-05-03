namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;

public interface IApiElement
{
    string Id { get; init; }
    string ContentType { get; }
}
