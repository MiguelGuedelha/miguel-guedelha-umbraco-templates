using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Changes;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

internal sealed class CacheInvalidationNotificationsHandler :
    INotificationAsyncHandler<ContentPublishedNotification>,
    INotificationAsyncHandler<ContentUnpublishedNotification>,
    INotificationAsyncHandler<ContentMovedNotification>,
    INotificationAsyncHandler<ContentMovedToRecycleBinNotification>,
    INotificationAsyncHandler<ContentDeletedNotification>,
    INotificationAsyncHandler<ContentCacheRefresherNotification>
{
    private readonly IFusionCache _fusionCache;
    private readonly IContentService _contentService;

    public CacheInvalidationNotificationsHandler(IFusionCacheProvider fusionCacheProvider,
        IContentService contentService)
    {
        _contentService = contentService;
        _fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
    }

    public async Task HandleAsync(ContentPublishedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
    }

    public async Task HandleAsync(ContentUnpublishedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
    }

    public async Task HandleAsync(ContentMovedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
    }

    public async Task HandleAsync(ContentMovedToRecycleBinNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
    }

    public async Task HandleAsync(ContentDeletedNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
    }

    public async Task HandleAsync(ContentCacheRefresherNotification notification, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);

        if (notification.MessageObject is not ContentCacheRefresher.JsonPayload[] payloads)
        {
            return;
        }

        var refreshData = payloads.Where(x => x.ChangeTypes is TreeChangeTypes.RefreshNode or TreeChangeTypes.RefreshBranch);

        var content = _contentService.GetByIds(refreshData.Select(x => x.Key).OfType<Guid>());
    }
}
