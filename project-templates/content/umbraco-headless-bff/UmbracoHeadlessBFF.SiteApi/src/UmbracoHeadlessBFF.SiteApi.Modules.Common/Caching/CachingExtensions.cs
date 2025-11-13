using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public static class CachingExtensions
{
    extension(FusionCacheEntryOptions options)
    {
        public void SetAllDurations(TimeSpan duration)
        {
            options.SetDuration(duration);
            options.SetDistributedCacheDuration(duration);
        }

        public void SetAllDurationsZero()
        {
            options.SetDurationZero();
            options.SetDistributedCacheDurationZero();
        }
    }
}
