using Umbraco.Cms.Core.Composing;
using Umbraco.Community.DataProtection.Composing;

namespace UmbracoBFFAstro.Cms.Web.Configuration.DataProtection;

internal sealed class DataProtectionComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddUmbracoDataProtection("UmbracoBFFAstro-cms");
    }
}
