namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Abstractions;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
