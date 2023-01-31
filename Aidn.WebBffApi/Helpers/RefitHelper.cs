using Refit;

namespace Aidn.WebBffApi.Helpers;

public static class RefitHelper
{
    public static RefitSettings GetCustomRefitSettings()
    {
        var options = SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();
 
        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(options)
        };
    }
}


