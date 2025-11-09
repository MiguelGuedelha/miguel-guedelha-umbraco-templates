namespace UmbracoHeadlessBFF.SharedModules.Common.Caching.Messaging;

public sealed record CacheInvalidationMessage
{
    public IReadOnlyCollection<CacheInvalidationItem> Items { get; private set; }
    public bool InvalidateAll { get; private set; }
    public CacheInvalidationType ObjectsType { get; private set; }


    public CacheInvalidationMessage(CacheInvalidationType objectsType, bool invalidateAll = false, IReadOnlyCollection<CacheInvalidationItem>? items = null)
    {
        Items = items ?? [];
        ObjectsType = objectsType;
        InvalidateAll = invalidateAll;
    }

    public sealed record CacheInvalidationItem
    {
        public required Guid Key { get; init; }
        public required string ContentTypeAlias { get; init; }
    }

    public enum CacheInvalidationType
    {
        Content = 0,
        Domain = 1,
        Media = 2
    }
}
