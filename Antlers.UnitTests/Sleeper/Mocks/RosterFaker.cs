using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks;

public class RosterFaker
{
    public Roster GenerateValidRoster()
    {
        var rosterFaker = new Faker<Roster>()
            .RuleFor(r => r.RosterId, f => f.Random.Number())
            .RuleFor(r => r.Players, f => f.Random.ArrayElements(new int[f.Random.Int(1, 25)].ToArray()))
            .RuleFor(r => r.Starters, f => f.Random.ArrayElements(new int[f.Random.Int(1, 11)].ToArray()))
            .RuleFor(r => r.Reserve, f => f.Random.ArrayElements(new int[f.Random.Int(1, 4)].ToArray()))
            .RuleFor(r => r.Taxi, f => f.Random.ArrayElements(new int[f.Random.Int(1, 5)].ToArray()))
            .RuleFor(r => r.Settings, f => new RosterSettings
            {
                Wins = f.Random.Number(),
                WaiverPosition = f.Random.Number(),
                WaiverBudgetUsed = f.Random.Number(),
                TotalMoves = f.Random.Number(),
                Ties = f.Random.Number(),
                Losses = f.Random.Number(),
                FantasyPoints = f.Random.Number(),
                FantasyPointsDecimal = f.Random.Number(),
                FantasyPointsAgainst = f.Random.Number(),
                FantasyPointsAgainstDecimal = f.Random.Number()
            })
            .RuleFor(r => r.OwnerId, f => f.Random.Number().ToString())
            .RuleFor(r => r.CoOwners, f => f.Random.ArrayElements(Enumerable.Range(1, f.Random.Int(1, 5)).Select(i => i.ToString()).ToArray()))
            .RuleFor(r => r.LeagueId, f => f.Random.Number().ToString());

        return rosterFaker.Generate();
    }
}