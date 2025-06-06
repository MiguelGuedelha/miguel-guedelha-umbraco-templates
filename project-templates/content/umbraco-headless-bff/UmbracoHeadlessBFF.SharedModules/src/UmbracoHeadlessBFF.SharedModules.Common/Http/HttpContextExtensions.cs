﻿using Microsoft.AspNetCore.Http;

namespace UmbracoHeadlessBFF.SharedModules.Common.Http;

public static class HttpContextExtensions
{
    public static void AddContextItem<T>(this HttpContext context, string key, T value)
    {
        context.Items[key] = value;
    }

    public static bool TryGetContextItem<T>(this HttpContext context, string key, out T? item)
    {
        var found = context.Items.TryGetValue(key, out var value);
        item = default;

        if (!found || value is not T castValue)
        {
            return false;
        }

        item = castValue;
        return true;

    }
}
