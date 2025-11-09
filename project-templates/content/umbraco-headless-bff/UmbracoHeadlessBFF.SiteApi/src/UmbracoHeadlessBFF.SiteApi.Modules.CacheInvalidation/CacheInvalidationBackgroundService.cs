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

    private const int MaxRetryCount = 3;

    private static readonly string[] s_genericTags =
    [
        CachingTagConstants.Sites,
        CachingTagConstants.Sitemaps,
        CachingTagConstants.Robots,
        CachingTagConstants.Redirects
    ];

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
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(args.Exception, "Failure during cache invalidation processing");
            }
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

        if (message is null || args.Message.DeliveryCount > MaxRetryCount)
        {
            // Message is wrongly formatted or can't be processed
            // Needs manual verification
            await args.DeadLetterMessageAsync(args.Message);
            return;
        }

        switch (message.ObjectsType)
        {
            case CacheInvalidationMessage.CacheInvalidationType.Domain:
                await _siteApiFusionCache.ClearAsync();
                await _siteApiOutputFusionCache.ClearAsync();
                break;

            case CacheInvalidationMessage.CacheInvalidationType.Media:
                switch (message.InvalidateAll)
                {
                    case true:
                        await _siteApiFusionCache.RemoveByTagAsync(CachingTagConstants.Media);
                        break;
                    case false:
                        await _siteApiFusionCache.RemoveByTagAsync(message.Items.Select(x => x.Key.ToString()));
                        break;
                }

                await _siteApiOutputFusionCache.ClearAsync();
                break;

            case CacheInvalidationMessage.CacheInvalidationType.Content:
                foreach (var item in message.Items)
                {
                    await _siteApiFusionCache.RemoveByTagAsync(item.Key.ToString());
                    await _siteApiOutputFusionCache.RemoveByTagAsync(item.Key.ToString());
                }

                await _siteApiFusionCache.RemoveByTagAsync(s_genericTags);
                await _siteApiOutputFusionCache.RemoveByTagAsync(s_genericTags);
                break;

            default:
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning("Unsupported CacheInvalidationMessage type: {Type}. Abandoning", message.ObjectsType);
                }

                await args.AbandonMessageAsync(args.Message);
                return;
        }

        await args.CompleteMessageAsync(args.Message);
    }
}
