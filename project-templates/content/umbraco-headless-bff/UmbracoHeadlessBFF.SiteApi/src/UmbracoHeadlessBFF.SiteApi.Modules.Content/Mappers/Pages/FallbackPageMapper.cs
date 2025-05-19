using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class FallbackPageMapper : IPageMapper
{
    public bool CanMap(string type) => true;

    public Task<IPage?> Map(IApiContent model)
    {
        return Task.FromResult<IPage?>(new FallbackPage
        {
            Context = new()
            {
                Seo = new(),
                Site = new()
                {
                    Locale = string.Empty,
                    Domain = string.Empty,
                    Subpath = string.Empty,
                    AlternateLanguages = []
                }
            },
            Content = new()
            {
                Id = model.Id,
                ContentType = model.ContentType,
                MainContent = [],
                AdditionalProperties = new()
            }
        });
    }
}
