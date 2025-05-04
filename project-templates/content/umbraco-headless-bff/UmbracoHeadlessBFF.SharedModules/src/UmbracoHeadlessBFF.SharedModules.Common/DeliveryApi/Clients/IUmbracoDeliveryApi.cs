using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Clients;

public interface IUmbracoDeliveryApi
{
    ApiResponse<IApiContent> GetContent(
        string? fetch = null,
        IEnumerable<string>? filter = null,
        string? sort = null,
        int skip = 0,
        int take = 10,
        string? expand = null,
        string fields = "properties[$all]",
        [Header("Accept-Language")] string? acceptLanguage = null,
        [Header("Preview")] bool preview = false,
        [Header("Start-Item")] string? startItem = null);
}
