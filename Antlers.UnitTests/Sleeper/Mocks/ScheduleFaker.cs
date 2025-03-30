using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks;

public class ScheduleFaker
{
    public Schedule GenerateValidSchedule()
    {
        var scheduleFaker = new Faker<Schedule>()
            .RuleFor(s => s.GameId, f => f.Random.Guid().ToString())
            .RuleFor(s => s.Week, f => f.Random.Int(1, 17))
            .RuleFor(s => s.Date, f => f.Date.Future().ToString("yyyy-MM-dd"))
            .RuleFor(s => s.Home, f => f.Address.City())
            .RuleFor(s => s.Away, f => f.Address.City())
            .RuleFor(s => s.Status, f => f.PickRandom(new[] { "Scheduled", "Completed", "Postponed" }));

        return scheduleFaker.Generate();
    }
}