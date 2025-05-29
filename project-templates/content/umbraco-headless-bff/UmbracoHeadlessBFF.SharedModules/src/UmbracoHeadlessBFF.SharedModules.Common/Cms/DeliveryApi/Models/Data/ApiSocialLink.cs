namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiSocialLink
{
    public string? Network { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
}
