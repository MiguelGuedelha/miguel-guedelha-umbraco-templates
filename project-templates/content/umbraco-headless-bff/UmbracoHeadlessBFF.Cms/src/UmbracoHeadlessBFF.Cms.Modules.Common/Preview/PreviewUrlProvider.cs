using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using UmbracoHeadlessBFF.Cms.Modules.Common.Links;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed class PreviewUrlProvider : IUrlProvider
{
    private readonly LinkService _linkService;
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
    private readonly PreviewOptions _previewOptions;
    private readonly ILogger<PreviewUrlProvider> _logger;

    public PreviewUrlProvider(
        LinkService linkService,
        IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
        IOptions<PreviewOptions> previewOptions,
        ILogger<PreviewUrlProvider> logger)
    {
        _linkService = linkService;
        _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        _previewOptions = previewOptions.Value;
        _logger = logger;
    }

    public string Alias => "PreviewProvider";

    public UrlInfo? GetUrl(IPublishedContent content, UrlMode mode, string? culture, Uri current) => null;

    public IEnumerable<UrlInfo> GetOtherUrls(int id, Uri current) => [];

    public Task<UrlInfo?> GetPreviewUrlAsync(IContent content, string? culture, string? segment)
    {
        if (string.IsNullOrWhiteSpace(_previewOptions.SecretKey) || Encoding.UTF8.GetByteCount(_previewOptions.SecretKey) < 64)
        {
            _logger.LogWarning("No preview secret configured or is under required 512 byte length, skipping headless preview URL generation");
            return Task.FromResult<UrlInfo?>(null);
        }

        if (culture is null)
        {
            return Task.FromResult<UrlInfo?>(null);
        }

        var link = _linkService.GetLinkByContentId(content.Key, culture, true);

        if (link is null)
        {
            return Task.FromResult<UrlInfo?>(null);
        }

        var domain = link.Authority.StartsWith("http") ? link.Authority.Replace("http:", "https:") : $"https://{link.Authority}";

        var uriBuilder = new UriBuilder(domain)
        {
            Path = link.Path,
            Query = $"?previewMode=true&previewToken={GenerateToken()}"
        };

        var cultureInfo = new CultureInfo(culture);

        return Task.FromResult<UrlInfo?>(new UrlInfo(
            uriBuilder.Uri,
            Alias,
            culture,
            $"Headless Preview - {cultureInfo.DisplayName}",
            true)
        );
    }

    private string GenerateToken()
    {
        var user = _backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser;

        if (user is null)
        {
            return string.Empty;
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_previewOptions.SecretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, "site-preview")]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(handler.CreateToken(tokenDescriptor));
    }
}
