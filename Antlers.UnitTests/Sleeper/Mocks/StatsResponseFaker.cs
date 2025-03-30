using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks;

public class StatsResponseFaker
{
    public StatsResponse GenerateValidStatsResponse()
    {
        var statsFaker = new StatsFaker();
        var playerFaker = new PlayerFaker();

        var statsResponseFaker = new Faker<StatsResponse>()
            .RuleFor(sr => sr.Date, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(sr => sr.Stats, f => statsFaker.GenerateValidStats())
            .RuleFor(sr => sr.Category, f => f.Lorem.Word())
            .RuleFor(sr => sr.LastModified, f => f.Date.Past().Ticks)
            .RuleFor(sr => sr.Week, f => f.Random.Int(1, 17))
            .RuleFor(sr => sr.Sport, f => f.PickRandom(new[] { "nfl", "nba", "mlb" }))
            .RuleFor(sr => sr.Season, f => f.Date.Past().Year.ToString())
            .RuleFor(sr => sr.SeasonType, f => f.PickRandom(new[] { "pre", "reg", "post" }))
            .RuleFor(sr => sr.PlayerId, f => f.Random.Guid().ToString())
            .RuleFor(sr => sr.GameId, f => f.Random.Guid().ToString())
            .RuleFor(sr => sr.UpdatedAt, f => f.Date.Past().Ticks)
            .RuleFor(sr => sr.Team, f => f.Address.City())
            .RuleFor(sr => sr.Company, f => f.Company.CompanyName())
            .RuleFor(sr => sr.Opponent, f => f.Address.City())
            .RuleFor(sr => sr.Player, f => playerFaker.GenerateValidPlayer());

        return statsResponseFaker.Generate();
    }
}