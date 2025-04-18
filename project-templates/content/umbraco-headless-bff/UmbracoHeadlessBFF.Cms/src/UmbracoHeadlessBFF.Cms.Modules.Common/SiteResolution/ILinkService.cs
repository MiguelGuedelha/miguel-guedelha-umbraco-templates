using UmbracoHeadlessBFF.SharedModules.Common.SiteResolution;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.SiteResolution;

internal interface ILinkService
{
    Link? GetLinkByContentId(Guid linkId, string culture, bool preview);
}
