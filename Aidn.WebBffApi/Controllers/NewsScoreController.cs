using Aidn.Shared;
using Aidn.WebBffApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Aidn.WebBffApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class NewsScoreController : ControllerBase
{
    private readonly IAidnMeasurementsApi _client;

    public NewsScoreController(IAidnMeasurementsApi client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<ActionResult<double>> Get(MeasurementsModel measurementsModel)
    {
        var response = await _client.GetNewsScore(measurementsModel);
        if (response.IsSuccessStatusCode)
        {
            return Ok(response.Content.Score);
        }

        return Problem(response.Error.Message);
    }
}

