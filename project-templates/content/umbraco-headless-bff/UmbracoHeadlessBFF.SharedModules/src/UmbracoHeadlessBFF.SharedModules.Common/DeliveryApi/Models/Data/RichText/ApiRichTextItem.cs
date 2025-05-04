namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.RichText;

/// <summary>
/// It does not include the blocks array/list, as do not recommend embedding blocks within RTE
/// </summary>
public sealed class ApiRichTextItem
{
    string? Markup { get; init; }
}
