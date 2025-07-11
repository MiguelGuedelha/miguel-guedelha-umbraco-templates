using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;

internal static class DeleteCacheByTagEndpoint
{
    public static RouteGroupBuilder MapDeleteCacheByTag(this RouteGroupBuilder builder)
    {
        builder
            .MapDelete("/tags/{tag}", Handle)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Ok> Handle(
        string tag,
        IFusionCacheProvider fusionCacheProvider,
        CancellationToken cancellationToken)
    {
        var fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);

        await fusionCache.RemoveByTagAsync(tag, token: cancellationToken);

        return TypedResults.Ok();
    }
}
