using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UmbracoHeadlessBFF.SharedModules.Common.Caching.Messaging;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;

public sealed class CacheInvalidationBackgroundService : BackgroundService
{
    private readonly ServiceBusProcessor _serviceBusProcessor;
    private readonly IFusionCache _siteApiFusionCache;
    private readonly IFusionCache _siteApiOutputFusionCache;
    private readonly ILogger<CacheInvalidationBackgroundService> _logger;

    public CacheInvalidationBackgroundService(
        ServiceBusClient serviceBusClient,
        IFusionCacheProvider fusionCacheProvider,
        ILogger<CacheInvalidationBackgroundService> logger)
    {
        _serviceBusProcessor = serviceBusClient.CreateProcessor(
            Services.ServiceBus.Topics.CmsCache,
            Services.ServiceBus.Subscriptions.SiteApiCmsCache,
            new()
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 5
            });

        _siteApiFusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
        _siteApiOutputFusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiOutputCacheName);
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceBusProcessor.ProcessMessageAsync += ProcessCacheInvalidationMessage;
        _serviceBusProcessor.ProcessErrorAsync += args =>
        {
            _logger.LogError(args.Exception, "Failure during cache invalidation processing");
            return Task.CompletedTask;
        };
        await _serviceBusProcessor.StartProcessingAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusProcessor.StopProcessingAsync(cancellationToken);
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
            await _siteApiFusionCache.ClearAsync();
            await _siteApiOutputFusionCache.ClearAsync();
        }

        foreach (var item in message.Items)
        {
            if (item.ContentTypeAlias == "siteSettings")
            {
                await _siteApiFusionCache.RemoveByTagAsync(CachingTagConstants.Sites);
            }

            await _siteApiFusionCache.RemoveByTagAsync(item.Key.ToString());
            await _siteApiOutputFusionCache.RemoveByTagAsync(item.Key.ToString());
        }

        await args.CompleteMessageAsync(args.Message);
    }
}
