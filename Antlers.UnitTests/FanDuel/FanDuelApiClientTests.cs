using Antlers.FanDuel;

namespace Antlers.UnitTests.FanDuel;

public class FanDuelApiClientTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_RejectsEmptyApplicationId(string applicationId)
    {
        Assert.Throws<ArgumentException>(() => new FanDuelApiClient(applicationId));
    }

    [Fact]
    public async Task GetMarketPrices_RejectsEmptyMarketIds()
    {
        using var client = new FanDuelApiClient("test-application");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetMarketPrices(Array.Empty<string>()));
    }

    [Fact]
    public async Task GetMarketPrices_RejectsBlankMarketId()
    {
        using var client = new FanDuelApiClient("test-application");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetMarketPrices(["719.123", " "]));
    }

    [Fact]
    public async Task GetMarketPrices_RejectsInvalidJurisdiction()
    {
        using var client = new FanDuelApiClient("test-application");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetMarketPrices("CA-ON", ["719.123"]));
    }

    [Fact]
    public async Task GetContentPage_RejectsInvalidJurisdiction()
    {
        using var client = new FanDuelApiClient("test-application");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetContentPage("US"));
    }
}
