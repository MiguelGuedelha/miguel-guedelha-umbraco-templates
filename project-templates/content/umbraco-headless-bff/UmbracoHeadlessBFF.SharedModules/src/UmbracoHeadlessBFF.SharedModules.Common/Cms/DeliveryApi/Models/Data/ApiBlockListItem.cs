namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiBlockListItem : IApiBlockListItem
{
    public required IApiElement Content { get; init; }
    public IApiElement? Settings { get; init; }
}

public sealed record ApiBlockListItem<T> : IApiBlockListItem<T>
    where T : class, IApiElement
{
    public required T Content { get; init; }
    public IApiElement? Settings { get; init; }
}
