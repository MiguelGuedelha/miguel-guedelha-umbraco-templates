﻿namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

/// <summary>
/// It does not include the blocks array/list, as do not recommend embedding blocks within RTE
/// </summary>
public sealed class ApiRichTextItem
{
    public string? Markup { get; init; }
}
