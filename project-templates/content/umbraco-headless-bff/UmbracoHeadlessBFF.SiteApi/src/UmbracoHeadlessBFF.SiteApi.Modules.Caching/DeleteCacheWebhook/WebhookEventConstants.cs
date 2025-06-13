namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching.DeleteCacheWebhook;

internal static class WebhookEventConstants
{
    public const string DomainSaved = "domainSaved";
    public const string DomainDeleted = "domainDeleted";
    public const string ContentPublished = "Umbraco.ContentPublish";
    public const string ContentUnpublished = "Umbraco.ContentUnpublish";
    public const string ContentMoved = "Umbraco.ContentMoved";
    public const string ContentMovedToRecycleBin = "Umbraco.ContentMovedToRecycleBin";
    public const string ContentDeleted = "Umbraco.ContentDelete"; }
