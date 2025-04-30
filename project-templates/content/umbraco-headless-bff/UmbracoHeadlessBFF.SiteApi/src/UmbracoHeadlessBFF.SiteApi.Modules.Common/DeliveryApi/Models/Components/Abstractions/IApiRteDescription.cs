namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Abstractions;

/// <summary>
/// It does not include the blocks array/list, as do not recommend embedding blocks within RTE
/// </summary>
public interface IApiRteDescription
{
    string? Markup { get; init; }
}
