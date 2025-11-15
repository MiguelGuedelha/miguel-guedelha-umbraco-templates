# Define excluded paths as an array
$EXCLUDED_PATHS_ARRAY = @(
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/umbraco'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/Views'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/wwwroot'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/uSync'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Modules.Common/Umbraco/Overrides'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Modules.Common/Umbraco/Models'
)

# Join them into a single space-separated string
$EXCLUDED_PATHS = $EXCLUDED_PATHS_ARRAY -join " "

dotnet restore
dotnet build --no-restore

dotnet format style --severity info --no-restore --verbosity normal --exclude $EXCLUDED_PATHS
dotnet format analyzers --severity info --no-restore --verbosity normal --exclude $EXCLUDED_PATHS
dotnet format whitespace --no-restore --verbosity normal --exclude $EXCLUDED_PATHS
