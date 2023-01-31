namespace Aidn.WebBffApi.Settings;

public class AppSettings
{
    public AzureAd AzureAd { get; init; } = new();
    public Uri? AidnMeasurementsApiUri { get; init; }
}

