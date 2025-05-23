namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
