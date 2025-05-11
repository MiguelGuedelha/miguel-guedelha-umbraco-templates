namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public static class CorrelationConstants
{
    public static class Headers
    {
        public const string CorrelationId = "x-correlation-id";
        public const string SiteHost = "x-site-host";
        public const string SitePath = "x-site-path";
        public const string SiteId = "x-site-id";
    }
}
