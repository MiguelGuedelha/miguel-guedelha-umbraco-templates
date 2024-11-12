using System.Diagnostics;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.DependencyInjection;
using Umbraco.Community.DataProtection.Composing;

namespace UmbracoTemplate.Web.Shared.Caching;

public class LoadBalancingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddServerRoleLoadBalancing();
        builder.AddUmbracoDataProtection();
    }
}

public static class ServerRoleExtensions
{
    public static void AddServerRoleLoadBalancing(this IUmbracoBuilder builder)
    {
        var serverRole = Environment.GetEnvironmentVariable("APPLICATION_SERVER_ROLE");

        var serverRoleParsed = !serverRole.IsNullOrWhiteSpace()
            ? Enum.Parse<ServerRole>(serverRole, true)
            : ServerRole.Single;

        switch (serverRoleParsed)
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
