using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks;

public class PlayerValuesFaker
{
    public Dictionary<string, decimal> GenerateValidPlayerValues()
    {
        return new Faker<Dictionary<string, decimal>>()
            .CustomInstantiator(f => new Dictionary<string, decimal>
            {
                [f.Random.Int(1000, 9999).ToString()] = Math.Round(f.Random.Decimal(-100m, 100m), 2),
                [f.Random.Int(1000, 9999).ToString()] = Math.Round(f.Random.Decimal(-100m, 100m), 2),
                [f.Random.Int(1000, 9999).ToString()] = Math.Round(f.Random.Decimal(-100m, 100m), 2)
            })
            .Generate();
    }
}
