using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Contracts;
using UmbracoHeadlessBFF.SharedModules.Common.Http;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

public sealed class SiteResolutionContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SiteResolutionContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsPreview
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            var exists = context.TryGetContextItem<bool>(SiteResolutionConstants.TenancyItems.IsPreview, out var isPreview);

            if (!exists)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No preview info present");
            }

            return isPreview;
        }
        set
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            context.TryAddContextItem(SiteResolutionConstants.TenancyItems.IsPreview, value);
        }
    }

    public string SiteId
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            var exists = context.TryGetContextItem<string>(SiteResolutionConstants.TenancyItems.SiteId, out var siteId);

            if (!exists)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No site id present");
            }

            return siteId!;
        }
        set
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            context.TryAddContextItem(SiteResolutionConstants.TenancyItems.SiteId, value);
        }
    }

    public SiteDefinition Site
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            var exists = context.TryGetContextItem<SiteDefinition>(SiteResolutionConstants.TenancyItems.Site, out var site);

            if (!exists)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No site id present");
            }

            return site!;
        }
        set
        {
            var context = _httpContextAccessor.HttpContext;

            if (context is null)
            {
                throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
            }

            context.TryAddContextItem(SiteResolutionConstants.TenancyItems.Site, value);
        }
    }
}
