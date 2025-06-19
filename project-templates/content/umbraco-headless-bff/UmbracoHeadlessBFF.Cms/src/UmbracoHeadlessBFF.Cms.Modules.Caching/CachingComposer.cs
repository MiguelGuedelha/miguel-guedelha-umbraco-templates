using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

internal sealed class CachingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationAsyncHandler<ContentPublishedNotification, CacheInvalidationNotificationsHandler>();
        builder.AddNotificationAsyncHandler<ContentUnpublishedNotification, CacheInvalidationNotificationsHandler>();
        builder.AddNotificationAsyncHandler<ContentMovedNotification, CacheInvalidationNotificationsHandler>();
        builder.AddNotificationAsyncHandler<ContentMovedToRecycleBinNotification, CacheInvalidationNotificationsHandler>();
        builder.AddNotificationAsyncHandler<ContentDeletedNotification, CacheInvalidationNotificationsHandler>();
        builder.AddNotificationAsyncHandler<ContentCacheRefresherNotification, CacheInvalidationNotificationsHandler>();
    }
}
