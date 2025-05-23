namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public interface IApiJumpMenuConfiguration
{
    string? JumpMenuHeading { get; init; }
    string? JumpMenuAnchorId { get; init; }
}
