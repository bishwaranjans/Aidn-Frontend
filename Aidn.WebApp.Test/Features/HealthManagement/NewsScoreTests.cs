using Aidn.Shared.Models;
using Aidn.WebApp.Test.Helpers;
using Aidn.WebBffApi.Client;
using Bunit;
using Bunit.TestDoubles;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using System.Security.Claims;

namespace Aidn.WebApp.Test.Features.HealthManagement;

public class NewsScoreTests : TestContext
{
    private readonly Mock<IAidnWebBffClient> _mockClient;

    public NewsScoreTests()
    {
        _mockClient = new Mock<IAidnWebBffClient>(MockBehavior.Strict);

    }

    [Fact]
    public void NewsScore_PageLoad_ShouldSuccess()
    {
        // Arrange
        using var ctx = new TestContext();
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("Test user");
        authContext.SetRoles("global");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "Test user"),
            new Claim("http://schemas.microsoft.com/identity/claims/scope", "access_as_user"),
        };

        authContext.SetClaims(claims);

        _mockClient.Setup(e => e.GetNewsScore(It.IsAny<IEnumerable<Measurement>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(3.ToApiResponse());

        ctx.Services.AddDefaultServices();
        ctx.Services.AddSingleton(_mockClient.Object);
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var comp = ctx.RenderComponent<Aidn.WebApp.Features.HealthMeasurement.NewsScore>();
        
        // Assert
        comp.HasComponent<MudCardContent>().Should().BeTrue();
        _mockClient.Verify();
    }
}


