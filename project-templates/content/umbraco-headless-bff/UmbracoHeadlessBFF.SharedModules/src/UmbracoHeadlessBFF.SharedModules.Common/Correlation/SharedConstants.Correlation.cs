// ReSharper disable once CheckNamespace
namespace UmbracoHeadlessBFF.SharedModules.Common;

public static partial class SharedConstants
{
    public static partial class Common
    {
        public static class Correlation
        {
            public static class Headers
            {
                public const string CorrelationId = "x-correlation-id";
                public const string SiteHost = "x-site-host";
                public const string SitePath = "x-site-path";
                public const string SiteId = "x-site-id";
            }
        }
    }
}
