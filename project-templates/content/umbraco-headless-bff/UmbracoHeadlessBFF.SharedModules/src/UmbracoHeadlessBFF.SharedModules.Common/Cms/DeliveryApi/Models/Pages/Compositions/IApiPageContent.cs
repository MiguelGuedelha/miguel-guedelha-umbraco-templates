﻿using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

public interface IApiPageContent
{
    public ApiBlockGrid MainContent { get; init; }
}
