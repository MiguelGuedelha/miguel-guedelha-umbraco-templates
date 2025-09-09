using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Changes;
using UmbracoHeadlessBFF.SharedModules.Common.Caching.Messaging;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

internal sealed class CacheInvalidationNotificationsHandler :
    INotificationAsyncHandler<ContentCacheRefresherNotification>,
    INotificationAsyncHandler<DomainCacheRefresherNotification>
{
    private readonly IContentService _contentService;
    private readonly ServiceBusSender _serviceBusSender;

    public CacheInvalidationNotificationsHandler(
        IContentService contentService,
        ServiceBusClient serviceBusClient)
    {
        _contentService = contentService;
        _serviceBusSender = serviceBusClient.CreateSender(Services.ServiceBus.Topics.CmsCache);
    }

    public async Task HandleAsync(ContentCacheRefresherNotification notification, CancellationToken cancellationToken)
    {
        if (notification.MessageObject is not ContentCacheRefresher.JsonPayload[] payloads)
        {
            return;
        }

        var items = new List<CacheInvalidationMessage.CacheInvalidationItem>();

        foreach (var payload in payloads.Where(x => x.ChangeTypes is not TreeChangeTypes.Remove and not TreeChangeTypes.None))
        {
            if (payload.ChangeTypes is TreeChangeTypes.RefreshAll)
            {
                items = null;
                break;
            }

            var content = _contentService.GetById(payload.Id);

            if (content is null)
            {
                break;
            }

            items.Add(new()
            {
                Key = content.Key,
                ContentTypeAlias = content.ContentType.Alias
            });

            var changeType = payload.ChangeTypes;

            if (changeType != TreeChangeTypes.RefreshBranch)
            {
                continue;
            }

            var descendants = _contentService.GetPagedDescendants(content.Id, 0, int.MaxValue, out _);
            items.AddRange(descendants
                .Select(descendant => new CacheInvalidationMessage.CacheInvalidationItem
                {
                    Key = descendant.Key,
                    ContentTypeAlias = descendant.ContentType.Alias
                }));
        }

        var message = items switch
        {
            not null => new(CacheInvalidationMessage.CacheInvalidationType.Content, false, items),
            _ => new CacheInvalidationMessage(CacheInvalidationMessage.CacheInvalidationType.Content, true)
        };

        var serialisedMessage = JsonSerializer.Serialize(message);

        await _serviceBusSender.SendMessageAsync(new(serialisedMessage), cancellationToken);
    }

    public async Task HandleAsync(DomainCacheRefresherNotification notification, CancellationToken cancellationToken)
    {
        if (notification.MessageObject is not DomainCacheRefresher.JsonPayload[] payloads)
        {
            return;
        }

        if (!payloads.Any(x => x.ChangeType is not DomainChangeTypes.None))
        {
            return;
        }

        var message = new CacheInvalidationMessage(CacheInvalidationMessage.CacheInvalidationType.Domain);

        var serialisedMessage = JsonSerializer.Serialize(message);

        await _serviceBusSender.SendMessageAsync(new(serialisedMessage), cancellationToken);
    }
}
