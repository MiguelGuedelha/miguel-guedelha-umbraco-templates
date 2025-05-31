namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

/// <summary>
/// It does not include the blocks array/list, as do not recommend embedding blocks within RTE
/// </summary>
public sealed record ApiRichTextItem
{
    public string? Markup { get; init; }
}
