namespace Aidn.WebBffApi.Settings;

public class AzureAd
{
    public string Instance { get; set; } = default!;
    public string Domain { get; set; } = default!;
    public string TenantId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string CallbackPath { get; set; } = default!;
    public string Scopes { get; set; } = default!;
}
