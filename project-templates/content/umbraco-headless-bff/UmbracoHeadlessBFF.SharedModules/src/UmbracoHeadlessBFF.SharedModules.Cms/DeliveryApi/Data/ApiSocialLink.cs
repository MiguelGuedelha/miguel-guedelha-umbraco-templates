namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiSocialLink
{
    public string? Network { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
}
