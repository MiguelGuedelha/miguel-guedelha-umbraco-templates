using System.Text;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

public static class DeliveryApiRequestHelper
{
    public static string GeneratePropertiesAllValue(int level = 1)
    {
        if (level < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be 1 or greater.");
        }

        const string basePart = "properties[$all";
        var builder = new StringBuilder();

        // Open brackets and inner base strings
        for (var i = 0; i < level - 1; i++)
        {
            builder.Append(basePart + "[");
        }

        // Final basePart without trailing bracket
        builder.Append(basePart);

        // Closing brackets
        builder.Append(new string(']', 1 + (level - 1) * 2));

        return builder.ToString();
    }
}
