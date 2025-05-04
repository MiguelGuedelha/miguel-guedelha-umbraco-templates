using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Converters;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

[JsonConverter(typeof(ApiContentConverter))]
public interface IApiContent
{
    Guid Id { get; init; }
    string Name { get; init; }
    DateTime CreateDate { get; init; }
    DateTime UpdateDate { get; init; }
    ApiContentRoute Route { get; init; }
    Dictionary<string, ApiContentRoute> Cultures { get; init; }
}

public interface IApiContent<T> : IApiContent
{
    T Properties { get; init; }
}

public abstract class ApiContent<T> : IApiContent<T>
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required T Properties { get; init; }
}

public abstract class ApiContent : IApiContent
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
}
