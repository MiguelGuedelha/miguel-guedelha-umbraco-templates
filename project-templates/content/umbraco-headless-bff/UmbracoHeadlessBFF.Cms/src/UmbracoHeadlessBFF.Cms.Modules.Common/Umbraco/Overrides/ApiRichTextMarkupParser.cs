using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Extensions;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Overrides;

internal sealed class ApiRichTextMarkupParser : IApiRichTextMarkupParser
{
    private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
    private readonly ILogger<ApiRichTextMarkupParser> _logger;

    public ApiRichTextMarkupParser(IPublishedSnapshotAccessor publishedSnapshotAccessor,
        ILogger<ApiRichTextMarkupParser> logger)
    {
        _publishedSnapshotAccessor = publishedSnapshotAccessor;
        _logger = logger;
    }

    public string Parse(string html)
    {
        try
        {
            var publishedSnapshot = _publishedSnapshotAccessor.GetRequiredPublishedSnapshot();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            ProcessLinks(doc);
            ProcessMedia(doc);

            return doc.DocumentNode.InnerHtml;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not parse rich text HTML, see exception for details");
            return html;
        }
    }

    private void ProcessLinks(HtmlDocument doc)
    {
        var links = doc.DocumentNode.SelectNodes("//a");

        if (links is null)
        {
            return;
        }

        foreach (var link in links)
        {
            link.Attributes.Remove("data-anchor");
            link.Attributes.Remove("title");
        }
    }

    private void ProcessMedia(HtmlDocument doc)
    {

    }
}
