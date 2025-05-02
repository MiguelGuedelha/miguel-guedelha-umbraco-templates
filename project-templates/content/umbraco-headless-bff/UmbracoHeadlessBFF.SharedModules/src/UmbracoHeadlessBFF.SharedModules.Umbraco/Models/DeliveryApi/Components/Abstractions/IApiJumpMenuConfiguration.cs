namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;

public interface IApiJumpMenuConfiguration
{
    string? JumpMenuHeading { get; init; }
    string? JumpMenuAnchorId { get; init; }
}
