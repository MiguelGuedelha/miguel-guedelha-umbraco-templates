﻿namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public interface IApiBlock<TContent, TSettings>
    where TContent : class, IApiElement
    where TSettings : class, IApiElement
{
    TContent Content { get; init; }
    TSettings Settings { get; init; }
}

public interface IApiBlock<TContent>
    where TContent : class, IApiElement
{
    TContent Content { get; init; }
}

public interface IApiBlock
{
    IApiElement Content { get; init; }
    IApiElement? Settings { get; init; }
}
