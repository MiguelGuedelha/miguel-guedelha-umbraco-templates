namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
