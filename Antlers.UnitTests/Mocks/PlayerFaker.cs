using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Mocks;

public class PlayerFaker
{
    public Player GenerateValidPlayer()
    {
        var playerFaker = new Faker<Player>()
            .RuleFor(p => p.PlayerId, f => f.Random.Guid().ToString())
            .RuleFor(p => p.FirstName, f => f.Random.String())
            .RuleFor(p => p.LastName, f => f.Random.String())
            .RuleFor(p => p.Number, f => f.Random.Number())
            .RuleFor(p => p.Position, f => f.Random.String())
            .RuleFor(p => p.FantasyPositions, f => f.Random.ArrayElements(new[] { "QB", "RB", "WR", "TE", "K", "DEF", "DL", "LB", "DB" }, f.Random.Int(1, 3)).ToArray())
            .RuleFor(p => p.Status, f => f.Random.String())
            .RuleFor(p => p.Age, f => f.Random.Int(20, 40))
            .RuleFor(p => p.Height, f => f.Random.Int(66, 84).ToString())
            .RuleFor(p => p.Weight, f => f.Random.Int(180, 350).ToString())
            .RuleFor(p => p.BirthDate, f => f.Date.Between(DateTime.Now.AddYears(-35), DateTime.Now.AddYears(-20)).ToString("yyyy-MM-dd"))
            .RuleFor(p => p.Sport, f => f.Random.String())
            .RuleFor(p => p.Team, f => f.Random.String())
            .RuleFor(p => p.College, f => f.Random.String())
            .RuleFor(p => p.HighSchool, f => f.Random.String())
            .RuleFor(p => p.DepthChartPosition, f => f.Random.String())
            .RuleFor(p => p.DepthChartOrder, f => f.Random.Number())
            .RuleFor(p => p.YearsExperience, f => f.Random.Int(1, 10))
            .RuleFor(p => p.InjuryStartDate, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
            .RuleFor(p => p.PracticeParticipation, f => f.Random.Bool())
            .RuleFor(p => p.InjuryStatus, f => f.Random.Bool())
            .RuleFor(p => p.SearchRank, f => f.Random.Number())
            .RuleFor(p => p.SearchFullName, f => f.Name.FullName())
            .RuleFor(p => p.SearchFirstName, f => f.Name.FirstName())
            .RuleFor(p => p.SearchLastName, f => f.Name.LastName())
            .RuleFor(p => p.Hashtag, f => f.Random.String())
            .RuleFor(p => p.FantasyDataId, f => f.Random.Number())
            .RuleFor(p => p.StatsId, f => f.Random.String())
            .RuleFor(p => p.SportRadarId, f => f.Random.String())
            .RuleFor(p => p.EspnId, f => f.Random.String())
            .RuleFor(p => p.RotowireId, f => f.Random.Number())
            .RuleFor(p => p.RotoworldId, f => f.Random.Number())
            .RuleFor(p => p.YahooId, f => f.Random.Number());

        return playerFaker.Generate();
    }
}