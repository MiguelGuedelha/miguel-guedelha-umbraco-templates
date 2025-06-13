using Umbraco.Cms.Core.Composing;

namespace UmbracoHeadlessBFF.Cms.Web.Composers;

internal sealed class WebhooksComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.WebhookEvents().Clear().AddCms();
    }
}
