using Aidn.Shared.Models;
using Aidn.WebBffApi.Client.Test.Helpers;
using AidnMeasurementsApi.WebApi.Client;
using Moq;
using Refit;

namespace Aidn.WebBffApi.Client.Test;

public class AidnWebBffClientTests : AidnTestWebHost<Program>
{
    private IAidnWebBffClient Client => RestService.For<IAidnWebBffClient>(GetClient());

    [Fact]
    public async Task AidnWebBffClient_GetNewsScore_Success()
    {
        // Arrange
        var measurements = new List<Measurement>
            {
                new() { MeasurementType = MeasurementType.TEMP, Value= 37 },
                new() { MeasurementType = MeasurementType.HR, Value= 60 },
                new() { MeasurementType = MeasurementType.RR, Value= 5 },
            };

        var mock = new Mock<IAidnMeasurementsApiClient>(MockBehavior.Strict);
        mock.Setup(x => x.Get(It.IsAny<IEnumerable<Measurement>>())).ReturnsAsync(new NewsScore(Score: 3).ToApiResponse());
        ReplaceService(mock.Object);

        // Act
        var result = await Client.GetNewsScore(measurements);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccessStatusCode);
        Assert.Equal(3, result.Content);

        mock.Verify(x => x.Get(It.IsAny<IEnumerable<Measurement>>()), Times.Once);
        mock.VerifyNoOtherCalls();
    }
}
