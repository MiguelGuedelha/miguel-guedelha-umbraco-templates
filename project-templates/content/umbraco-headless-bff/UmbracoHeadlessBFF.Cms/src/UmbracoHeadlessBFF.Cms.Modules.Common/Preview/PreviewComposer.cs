using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

public sealed class PreviewComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        //TODO: Preview URL generation
        // builder.AddNotificationHandler<SendingContentNotification, PreviewUrlNotificationHandler>();
    }
}
