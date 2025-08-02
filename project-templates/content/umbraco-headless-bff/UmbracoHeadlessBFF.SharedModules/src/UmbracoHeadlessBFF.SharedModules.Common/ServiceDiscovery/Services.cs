namespace UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

public static class Services {
    public const string SmtpServer = "MailServer";
    public const string DatabaseServer = "SqlServer";
    public const string Database = "Database";
    public const string AzureStorage = "Storage";
    public const string AzureBlobs = "blobs";

    public static class ServiceBus {
        public const string Name = "ServiceBus";

        public static class Topics {
            public const string CmsCache = "CmsCacheTopic";
        }

        public static class Subscriptions {
            public const string SiteApiCmsCache = "CmsCacheTopicSiteApiSub";
        }
    }

    public const string Cms = "Cms";
    public const string SiteApi = "SiteApi";
    // public const string Frontend = "Frontend";
}
