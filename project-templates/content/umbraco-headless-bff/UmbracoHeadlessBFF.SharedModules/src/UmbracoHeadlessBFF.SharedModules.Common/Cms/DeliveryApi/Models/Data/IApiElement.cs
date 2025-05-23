namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public interface IApiElement
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}

public interface IApiElement<T> : IApiElement
{
    T Properties { get; init; }
}

public abstract class ApiElement : IApiElement
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}

public abstract class ApiElement<T> : IApiElement<T>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required T Properties { get; init; }
}
