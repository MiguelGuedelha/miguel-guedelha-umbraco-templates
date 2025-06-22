using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Caching.Messaging;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching;

public sealed class CacheInvalidationBackgroundService : BackgroundService
{
    private readonly ServiceBusProcessor _serviceBusProcessor;
    private readonly IFusionCache _fusionCache;

    public CacheInvalidationBackgroundService(ServiceBusClient serviceBusClient, IFusionCacheProvider fusionCacheProvider)
    {
        _serviceBusProcessor = serviceBusClient.CreateProcessor(
            "CmsCacheTopic",
            "CmsCacheTopicSiteApiSub",
            new()
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 5
            });

        _fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceBusProcessor.ProcessMessageAsync += ProcessCacheInvalidationMessage;
        await _serviceBusProcessor.StartProcessingAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusProcessor.StopProcessingAsync(cancellationToken);
        _serviceBusProcessor.ProcessMessageAsync -= ProcessCacheInvalidationMessage;
        await _serviceBusProcessor.CloseAsync(cancellationToken);
    }

    private async Task ProcessCacheInvalidationMessage(ProcessMessageEventArgs args)
    {
        var message = args.Message.Body.ToObjectFromJson<CacheInvalidationMessage>();

        if (message is null)
        {
            // Message is wrongly formatted or can't be processed
            // Needs manual verification
            await args.DeadLetterMessageAsync(args.Message);
            return;
        }

        if (message.ObjectsType is CacheInvalidationMessage.CacheInvalidationType.Domain || message.InvalidateAll)
        {
            await _fusionCache.ClearAsync();
        }

        foreach (var item in message.Items)
        {
            if (item.ContentTypeAlias == "siteSettings")
            {
                await _fusionCache.RemoveByTagAsync(CacheTagConstants.Sites);
            }

            await _fusionCache.RemoveByTagAsync(item.Key.ToString());
        }

        await args.CompleteMessageAsync(args.Message);
    }
}
