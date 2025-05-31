namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components.Compositions;

public interface IApiJumpMenuConfiguration
{
    string? JumpMenuHeading { get; init; }
    string? JumpMenuAnchorId { get; init; }
}
