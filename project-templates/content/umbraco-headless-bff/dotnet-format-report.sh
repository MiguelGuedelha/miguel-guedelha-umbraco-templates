#!/usr/bin/env bash

EXCLUDED_PATHS_ARRAY=(
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/umbraco/'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/Views/'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/wwwroot/'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Web/uSync/'
    './UmbracoHeadlessBFF.Cms/src/UmbracoHeadlessBFF.Cms.Modules.Common/Umbraco/'
)

EXCLUDE_ARGS=()
for path in "${EXCLUDED_PATHS_ARRAY[@]}"; do
    EXCLUDE_ARGS+=( "--exclude" "$path" )
done

echo "Exclude args: ${EXCLUDE_ARGS[*]}"

dotnet restore
dotnet build --no-restore

dotnet format style . --severity info --no-restore --verify-no-changes --verbosity normal "${EXCLUDE_ARGS[@]}"
dotnet format analyzers . --severity info --no-restore --verify-no-changes --verbosity normal "${EXCLUDE_ARGS[@]}"
dotnet format whitespace . --no-restore --verify-no-changes --verbosity normal "${EXCLUDE_ARGS[@]}"
