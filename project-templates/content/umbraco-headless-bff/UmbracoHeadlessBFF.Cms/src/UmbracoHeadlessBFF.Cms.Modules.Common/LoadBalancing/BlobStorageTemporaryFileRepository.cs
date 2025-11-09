using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.Models.TemporaryFile;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.LoadBalancing;

internal sealed class BlobStorageTemporaryFileRepository : ITemporaryFileRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    private const string BasePath = "umb-temp-files";

    private const string ExpireProperty = "umb-temp-expires-on";
    private const string FilenameProperty = "umb-file-name";

    public BlobStorageTemporaryFileRepository(
        BlobServiceClient blobServiceClient,
        IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        var containerName = configuration["Umbraco:Storage:AzureBlob:Media:ContainerName"];

        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentException("No container name configured");
        }

        _containerName = containerName;
    }

    public async Task<TemporaryFileModel?> GetAsync(Guid key)
    {
        var client = await GetBlobClient(key);

        if (!await client.ExistsAsync())
        {
            return null;
        }

        var tags = await client.GetTagsAsync();

        if (tags is null)
        {
            return null;
        }

        var hasValue = tags.Value.Tags.TryGetValue(ExpireProperty, out var expires);
        var hasParsedExpires = DateTime.TryParse(expires, out var parsedExpires);

        if (!hasValue
            || !hasParsedExpires
            || DateTime.UtcNow > parsedExpires.ToUniversalTime())
        {
            return null;
        }

        var stream = await client.OpenReadAsync();

        if (stream is null)
        {
            return null;
        }

        var hasFileName = tags.Value.Tags.TryGetValue(FilenameProperty, out var fileName);

        if (!hasFileName)
        {
            return null;
        }

        return new()
        {
            AvailableUntil = parsedExpires,
            FileName = fileName!,
            Key = key,
            OpenReadStream = () => stream
        };
    }

    public async Task SaveAsync(TemporaryFileModel model)
    {
        var client = await GetBlobClient(model.Key);

        var options = new BlobUploadOptions
        {
            Tags = new Dictionary<string, string>
            {
                { ExpireProperty, model.AvailableUntil.ToIsoString() },
                { FilenameProperty, model.FileName }
            }
        };

        await client.UploadAsync(model.OpenReadStream.Invoke(), options);
    }

    public async Task DeleteAsync(Guid key)
    {
        var client = await GetBlobClient(key);

        await client.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
    }

    public async Task<IEnumerable<Guid>> CleanUpOldTempFiles(DateTime dateTime)
    {
        var client = await GetContainerClient();

        var blobs = client.GetBlobsAsync(prefix: BasePath);

        var deleted = new List<Guid>();

        await foreach (var blob in blobs)
        {
            var hasExpires = blob.Tags.TryGetValue(ExpireProperty, out var expires);

            if (!hasExpires
                || !DateTime.TryParse(expires!, out var expiresParsed)
                || expiresParsed.ToUniversalTime() > dateTime.ToUniversalTime())
            {
                continue;
            }

            var response = await client.DeleteBlobAsync(blob.Name);
            if (!response.IsError && Guid.TryParse(blob.Name.Split('/').Last(), out var guid))
            {
                deleted.Add(guid);
            }
        }

        return deleted;
    }

    private async Task<BlobContainerClient> GetContainerClient()
    {
        var client = _blobServiceClient.GetBlobContainerClient(_containerName);

        if (!await client.ExistsAsync())
        {
            await client.CreateIfNotExistsAsync();
        }

        return client;
    }

    private async Task<BlobClient> GetBlobClient(Guid key)
    {
        var client = await GetContainerClient();

        var filePath = BasePath.CombineUri(key.ToString()!);

        return client.GetBlobClient(filePath);
    }
}
