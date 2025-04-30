namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Abstractions;

public interface IApiJumpMenuConfiguration
{
    string? JumpMenuHeading { get; init; }
    string? JumpMenuAnchorId { get; init; }
}
