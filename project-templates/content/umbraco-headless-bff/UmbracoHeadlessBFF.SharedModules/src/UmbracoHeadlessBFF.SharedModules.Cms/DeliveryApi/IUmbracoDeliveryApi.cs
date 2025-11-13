using Refit;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;

public interface IUmbracoDeliveryApi
{
    [Get("/content")]
    Task<ApiResponse<PagedApiContent>> GetContent(
        string? fetch = null,
        IEnumerable<string>? filter = null,
        string? sort = null,
        int skip = 0,
        int take = 10,
        string? expand = null,
        string fields = "properties[$all]",
        [Header("Accept-Language")] string? acceptLanguage = null,
        [Header("Preview")] bool preview = false,
        [Header("Start-Item")] string? startItem = null,
        CancellationToken cancellationToken = default);

    [Get("/content/item/{path}")]
    Task<ApiResponse<IApiContent>> GetItemByPath(
        string path,
        string? expand = null,
        string? fields = "properties[$all]",
        [Header("Accept-Language")] string? acceptLanguage = null,
        [Header("Preview")] bool preview = false,
        [Header("Start-Item")] string? startItem = null,
        CancellationToken cancellationToken = default);

    [Get("/content/item/{id}")]
    Task<ApiResponse<IApiContent>> GetItemById(
        Guid id,
        string? expand = null,
        string? fields = "properties[$all]",
        [Header("Accept-Language")] string? acceptLanguage = null,
        [Header("Preview")] bool preview = false,
        [Header("Start-Item")] string? startItem = null,
        CancellationToken cancellationToken = default);

    [Get("/content/items")]
    Task<ApiResponse<IReadOnlyCollection<IApiContent>>> GetItems(
        IEnumerable<Guid> id,
        string? expand = null,
        string? fields = "properties[$all]",
        [Header("Accept-Language")] string? acceptLanguage = null,
        [Header("Preview")] bool preview = false,
        [Header("Start-Item")] string? startItem = null,
        CancellationToken cancellationToken = default);
}
