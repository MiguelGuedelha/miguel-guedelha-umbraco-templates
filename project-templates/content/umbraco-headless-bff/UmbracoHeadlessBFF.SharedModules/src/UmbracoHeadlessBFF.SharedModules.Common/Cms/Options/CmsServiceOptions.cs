namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;

public sealed class CmsServiceOptions
{
    public const string SectionName = "services:Cms";
    public required string[] Https { get; init; }
    public required CmsServiceParameters Parameters { get; init; }
}

public sealed class CmsServiceParameters
{
    public required string DeliveryApiKey { get; init; }
}
