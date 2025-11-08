using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed class PreviewComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddUrlProvider<PreviewUrlProvider>();
    }
}
