using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;
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
        get => GetContextItem<bool>(SiteResolutionConstants.TenancyItems.IsPreview);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.IsPreview, value);
    }

    public string PreviewToken
    {
        get => GetContextItem<string>(SiteResolutionConstants.TenancyItems.PreviewToken);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.PreviewToken, value);
    }

    public string SiteId
    {
        get => GetContextItem<string>(SiteResolutionConstants.TenancyItems.SiteId);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.SiteId, value);
    }

    public string Domain
    {
        get => GetContextItem<string>(SiteResolutionConstants.TenancyItems.Domain);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.Domain, value);
    }

    public string Path
    {
        get => GetContextItem<string>(SiteResolutionConstants.TenancyItems.Path);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.Path, value);
    }

    public SiteDefinition Site
    {
        get => GetContextItem<SiteDefinition>(SiteResolutionConstants.TenancyItems.Site);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.Site, value);
    }

    public Guid PageId
    {
        get => GetContextItem<Guid>(SiteResolutionConstants.TenancyItems.PageId);
        set => SetContextItem(SiteResolutionConstants.TenancyItems.AlternateSites, value);
    }

    private T GetContextItem<T>(string key)
    {
        var context = _httpContextAccessor.HttpContext ?? throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
        var exists = context.TryGetContextItem<T>(key, out var value);

        if (!exists)
        {
            throw new SiteApiException(StatusCodes.Status404NotFound, $"No {key} present");
        }

        return value!;
    }

    private void SetContextItem<T>(string key, T value)
    {
        var context = _httpContextAccessor.HttpContext ?? throw new SiteApiException(StatusCodes.Status404NotFound, "No http context found");
        context.AddContextItem(key, value);
    }
}
