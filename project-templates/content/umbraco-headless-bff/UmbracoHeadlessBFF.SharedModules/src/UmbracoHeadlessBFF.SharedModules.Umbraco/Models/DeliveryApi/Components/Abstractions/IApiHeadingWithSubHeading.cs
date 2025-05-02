namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;

public interface IApiHeadingWithSubHeading : IApiHeading
{
    string? SubHeading { get; init; }
}
