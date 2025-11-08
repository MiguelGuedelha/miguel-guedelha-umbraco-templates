EXCLUDED_PATHS_ARRAY=(
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/umbraco'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/Views'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/wwwroot'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/uSync'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Modules.Common/Umbraco/Overrides'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Modules.Common/Umbraco/Models'
)

EXCLUDED_PATHS=""
for path in "${EXCLUDED_PATHS_ARRAY[@]}"; do
    EXCLUDED_PATHS+="$path "
done
EXCLUDED_PATHS=${EXCLUDED_PATHS%?}

dotnet restore
dotnet build --no-restore
dotnet format style --severity info --no-restore --verify-no-changes --verbosity normal --exclude ${EXCLUDED_PATHS}
dotnet format analyzers --severity info --no-restore --verify-no-changes --verbosity normal --exclude ${EXCLUDED_PATHS}
dotnet format whitespace --no-restore --verify-no-changes --verbosity normal --exclude ${EXCLUDED_PATHS}