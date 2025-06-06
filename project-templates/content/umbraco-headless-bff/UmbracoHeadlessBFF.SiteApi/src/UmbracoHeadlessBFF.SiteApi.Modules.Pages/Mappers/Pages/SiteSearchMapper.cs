﻿using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;

internal sealed class SiteSearchMapper : IPageMapper
{
    private readonly BasePageMapper _basePageMapper;

    public SiteSearchMapper(BasePageMapper basePageMapper)
    {
        _basePageMapper = basePageMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiSiteSearch;

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not ApiSiteSearch apiModel)
        {
            return null;
        }

        return new SiteSearch
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Context = await _basePageMapper.MapPageContext(apiModel)
        };
    }
}
