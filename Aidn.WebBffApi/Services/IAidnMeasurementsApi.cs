using Aidn.Shared;
using Refit;

namespace Aidn.WebBffApi.Services;

public interface IAidnMeasurementsApi
{
    [Post("/NewsScore")]
    Task<ApiResponse<NewsScore>> GetNewsScore([Body] MeasurementsModel measurementsModel);
}

