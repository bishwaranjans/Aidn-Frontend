using Aidn.Shared.Models;
using Refit;

namespace Aidn.WebBffApi.Client;

public interface IAidnWebBffClient
{
    [Post("/NewsScore")]
    Task<IApiResponse<int>> GetNewsScore([Body] IEnumerable<Measurement> measurements, CancellationToken token = default);
}
