using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.DependencyInjection;

namespace UmbracoHeadlessBFF.Cms.Web.Configuration.LoadBalancing;

internal sealed class LoadBalancingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // builder.Services.AddUnique<ITemporaryFileRepository, BlobStorageTemporaryFileRepository>();

        builder
            .SetServerRegistrar(new StaticServerAccessor())
            .LoadBalanceIsolatedCaches();

        var connectionString = builder.Config.GetUmbracoConnectionString();
        if (connectionString is null)
        {
            return;
        }

        builder.Services.AddSignalR().AddSqlServer(connectionString);

    }

    public class StaticServerAccessor : IServerRoleAccessor
    {
        public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
    }
}
