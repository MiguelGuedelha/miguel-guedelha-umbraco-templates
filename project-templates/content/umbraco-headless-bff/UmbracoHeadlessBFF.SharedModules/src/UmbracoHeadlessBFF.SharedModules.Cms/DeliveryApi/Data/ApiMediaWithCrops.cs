﻿namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiMediaWithCrops
{
    public required string Name { get; init; }
    public required string MediaType { get; init; }
    public required string Url { get; init; }
    public string? Extension { get; init; }
    public int? Width { get; init; }
    public int? Height { get; init; }
    public int? Bytes { get; init; }
    public ApiImageFocalPoint? FocalPoint { get; init; }
    public IReadOnlyCollection<ApiImageCrop>? Crops { get; init; }
    public IDictionary<string, object>? Properties { get; init; }
}
