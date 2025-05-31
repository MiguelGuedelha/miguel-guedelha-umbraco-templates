namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components.Compositions;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
