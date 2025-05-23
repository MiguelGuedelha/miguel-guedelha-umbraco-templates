namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public interface IApiElement
{
    Guid Id { get; init; }
    string ContentType { get; }
}

public interface IApiElement<T> : IApiElement
{
    T Properties { get; init; }
}
