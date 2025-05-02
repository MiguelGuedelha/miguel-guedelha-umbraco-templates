namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.RichText;

/// <summary>
/// It does not include the blocks array/list, as do not recommend embedding blocks within RTE
/// </summary>
public class ApiRichTextItem
{
    string? Markup { get; init; }
}
