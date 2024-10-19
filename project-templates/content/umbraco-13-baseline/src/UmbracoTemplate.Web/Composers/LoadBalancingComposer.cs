using System.Diagnostics;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.DependencyInjection;

namespace UmbracoTemplate.Web.Composers;

public class LoadBalancingComposer : IComposer
{
    private readonly ServerRole _serverRole;

    public LoadBalancingComposer()
    {
        var serverRole = Environment.GetEnvironmentVariable("APPLICATION_SERVER_ROLE");

        _serverRole = !serverRole.IsNullOrWhiteSpace()
            ? Enum.Parse<ServerRole>(serverRole, true)
            : ServerRole.Single;
    }

    public void Compose(IUmbracoBuilder builder)
    {
        switch (_serverRole)
        {
            case ServerRole.Single:
                builder.SetServerRegistrar<SingleServerRoleAccessor>();
                break;
            case ServerRole.Subscriber:
                builder.SetServerRegistrar<SubscriberServerRoleAccessor>();
                break;
            case ServerRole.SchedulingPublisher:
                builder.SetServerRegistrar<SchedulingPublisherServerRoleAccessor>();
                break;
            case ServerRole.Unknown:
            default:
                throw new UnreachableException("The server role type should be valid at this point");
        }
    }

    private class SchedulingPublisherServerRoleAccessor : IServerRoleAccessor
    {
        public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
    }

    private class SubscriberServerRoleAccessor : IServerRoleAccessor
    {
        public ServerRole CurrentServerRole => ServerRole.Subscriber;
    }
}
