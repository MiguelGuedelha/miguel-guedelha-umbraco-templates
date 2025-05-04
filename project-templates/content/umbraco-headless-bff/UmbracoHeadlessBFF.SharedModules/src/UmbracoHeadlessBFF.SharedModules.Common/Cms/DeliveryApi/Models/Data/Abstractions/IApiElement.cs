using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Converters;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;

[JsonConverter(typeof(ApiElementConverter))]
public interface IApiElement
{
    string Id { get; init; }
}

public interface IApiElement<T> : IApiElement
{
    T Properties { get; init; }
}

public abstract class ApiElement : IApiElement
{
    public required string Id { get; init; }
}

public abstract class ApiElement<T> : IApiElement<T>
{
    public required string Id { get; init; }
    public required T Properties { get; init; }
}
