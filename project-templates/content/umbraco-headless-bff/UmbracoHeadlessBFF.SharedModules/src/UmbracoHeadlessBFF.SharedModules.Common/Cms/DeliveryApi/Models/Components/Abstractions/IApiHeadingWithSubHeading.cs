namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Abstractions;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
