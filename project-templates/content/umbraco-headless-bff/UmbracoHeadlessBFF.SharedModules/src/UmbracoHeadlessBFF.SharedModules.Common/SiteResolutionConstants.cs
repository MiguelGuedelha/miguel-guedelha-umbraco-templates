namespace UmbracoHeadlessBFF.SharedModules.Common;

public static partial class SharedConstants
{
    public static partial class Common
    {
        public static class SiteResolution
        {
            public static class Headers
            {
                public const string SiteHost = "x-site-host";
                public const string SitePath = "x-site-path";
                public const string SiteId = "x-site-id";
            }

            public static class Cookies
            {
                public const string SiteId = "site-id";
            }
        }
    }
}
