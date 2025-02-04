using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks;

public class PlayerFaker
{
    public Player GenerateValidPlayer()
    {
        var playerFaker = new Faker<Player>()
            .RuleFor(p => p.PlayerId, f => f.Random.Guid().ToString())
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Number, f => f.Random.Int())
            .RuleFor(p => p.Position, f => f.Random.ArrayElement(new[] { "WR", "RB", "QB", "TE", "DT", "DE", "LB", "CB", "DB" }))
            .RuleFor(p => p.FantasyPositions, f => f.Random.ArrayElements(new[] { "QB", "RB", "WR", "TE", "K", "DEF", "DL", "LB", "DB" }, f.Random.Int(1, 3)).ToArray())
            .RuleFor(p => p.Status, f => f.Random.ArrayElement(new[] { "Active", "Inactive", "Exempt", "Free Agent", "Injured Reserve" }))
            .RuleFor(p => p.Age, f => f.Random.Int(20, 40))
            .RuleFor(p => p.Height, f => f.Random.Int(66, 84).ToString())
            .RuleFor(p => p.Weight, f => f.Random.Int(180, 350).ToString())
            .RuleFor(p => p.BirthDate, f => f.Date.Between(DateTime.Now.AddYears(-35), DateTime.Now.AddYears(-20)).ToString("yyyy-MM-dd"))
            .RuleFor(p => p.Sport, f => f.Random.Word())
            .RuleFor(p => p.Team, f => f.Random.String2(20))
            .RuleFor(p => p.TeamAbbreviation, f => f.Random.String2(5))
            .RuleFor(p => p.College, f => f.Company.CompanyName())
            .RuleFor(p => p.HighSchool, f => f.Company.CompanyName())
            .RuleFor(p => p.DepthChartPosition, f => f.Random.ArrayElement(new[] { "WR", "SWR", "QB", "DB", "CB", "LB", "RB", "TE" }))
            .RuleFor(p => p.DepthChartOrder, f => f.Random.Number())
            .RuleFor(p => p.YearsExperience, f => f.Random.Int(1, 10))
            .RuleFor(p => p.PracticeParticipation, f => f.Random.Bool())
            .RuleFor(p => p.InjuryStartDate, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now).ToString("yyyy-MM-dd"))
            .RuleFor(p => p.InjuryStatus, f => f.Random.Bool())
            .RuleFor(p => p.InjuryBodyPart, f => f.Random.ArrayElement(new[] { "Arm", "Knee", "ACL", "Hamstring", "MCL", "Achilles", "Concussion" }))
            .RuleFor(p => p.InjuryNotes, f => f.Lorem.Random.ToString())
            .RuleFor(p => p.SearchRank, f => f.Random.Number())
            .RuleFor(p => p.SearchFullName, f => f.Name.FullName())
            .RuleFor(p => p.SearchFirstName, f => f.Name.FirstName())
            .RuleFor(p => p.SearchLastName, f => f.Name.LastName())
            .RuleFor(p => p.Hashtag, f => f.Random.String2(20))
            .RuleFor(p => p.FantasyDataId, f => f.Random.Number())
            .RuleFor(p => p.StatsId, f => f.Random.Number())
            .RuleFor(p => p.SportRadarId, f => f.Random.Guid().ToString())
            .RuleFor(p => p.EspnId, f => f.Random.Number())
            .RuleFor(p => p.RotowireId, f => f.Random.Number())
            .RuleFor(p => p.RotoworldId, f => f.Random.Number())
            .RuleFor(p => p.YahooId, f => f.Random.Number())
            .RuleFor(p => p.OddsJamId, f => f.Random.String2(20))
            .RuleFor(p => p.PandaScoreId, f => f.Random.String2(20))
            .RuleFor(p => p.GsisId, f => f.Random.String2(20))
            .RuleFor(p => p.Metadata, f => new PlayerMetadata
            {
                ChannelId = f.Random.String2(20),
                RookieYear = f.Random.Int(2000, 2025).ToString()
            }); ;

        return playerFaker.Generate();
    }
}