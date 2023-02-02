using Aidn.Shared.Models;
using AidnMeasurementsApi.WebApi.Client;
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
    private readonly IAidnMeasurementsApiClient _client;

    public NewsScoreController(IAidnMeasurementsApiClient client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Get([FromBody] IEnumerable<Measurement> measurements)
    {
        var response = await _client.Get(measurements);
        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            return Ok(response.Content.Score);
        }

        return Problem(response.Error?.Message);
    }
}

