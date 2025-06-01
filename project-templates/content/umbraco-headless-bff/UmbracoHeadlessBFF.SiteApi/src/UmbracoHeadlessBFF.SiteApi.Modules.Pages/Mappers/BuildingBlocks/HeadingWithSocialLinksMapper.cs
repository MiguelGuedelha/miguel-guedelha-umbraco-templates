using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

internal interface IHeadingWithSocialLinksMapper : IMapper<ApiHeadingWithSocialLinks, HeadingWithSocialLinks>
{
}

internal sealed class HeadingWithSocialLinksMapper : IHeadingWithSocialLinksMapper
{

    public Task<HeadingWithSocialLinks?> Map(ApiHeadingWithSocialLinks model)
    {
        return Task.FromResult<HeadingWithSocialLinks?>(new()
        {
            Heading = model.Properties.Heading,
            HeadingSize = model.Properties.HeadingSize,
            SocialLinks = model.Properties.SocialLinks?.Select(x => new SocialLink
            {
                Name = x.Name,
                Network = x.Network,
                Url = x.Url
            }).ToArray()
        });
    }
}
