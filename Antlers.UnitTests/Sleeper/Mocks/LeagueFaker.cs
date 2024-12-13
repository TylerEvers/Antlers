using Antlers.Sleeper.Models;
using Bogus;

namespace Antlers.UnitTests.Sleeper.Mocks
{
    public class LeagueFaker
    {
        public League GenerateValidLeague()
        {
            var leagueFaker = new Faker<League>()
                .RuleFor(l => l.LeagueId, f => f.Random.Guid().ToString())
                .RuleFor(l => l.PreviousLeagueId, f => f.Random.Guid().ToString())
                .RuleFor(l => l.Name, f => f.Random.String())
                .RuleFor(l => l.Status, f => f.Random.String())
                .RuleFor(l => l.Settings, f => new LeagueSettings
                {
                    //League settings constructor that uses faker to generate test values
                    BestBall = f.Random.Int(0, 1),
                    LastReport = f.Random.Int(0, 1),
                    WaiverBudget = f.Random.Int(0, 200),
                    DisableAdds = f.Random.Int(0, 1),
                    CapacityOverride = f.Random.Int(0, 1),
                    WaiverBidMin = f.Random.Int(0, 10),
                    TaxiDeadline = f.Random.Int(0, 1),
                    DraftRounds = f.Random.Int(2, 5),
                    PositionLimitLb = f.Random.Int(0, 4),
                    ReserveAllowNa = f.Random.Int(0, 1),
                    StartWeek = f.Random.Int(1, 17),
                    PlayoffSeedType = f.Random.Int(0, 1),
                    PlayoffTeams = f.Random.Int(0, 12),
                    VetoVotesNeeded = f.Random.Int(0, 5),
                    Squads = f.Random.Int(0, 2),
                    NumTeams = f.Random.Int(4, 16),
                    DailyWaiversHour = f.Random.Int(0, 23),
                    PlayoffType = f.Random.Int(0, 1),
                    TaxiSlots = f.Random.Int(0, 5),
                    SubStartTimeEligibility = f.Random.Int(0, 1),
                    LastScoredLeg = f.Random.Int(0, 20),
                    DailyWaiversDays = f.Random.Int(1, 7),
                    SubLockIfStarterActive = f.Random.Int(0, 1),
                    PlayoffWeekStart = f.Random.Int(0, 17),
                    PositionLimitDb = f.Random.Int(0, 4),
                    WaiverClearDays = f.Random.Int(0, 7),
                    ReserveAllowDoubtful = f.Random.Int(0, 1),
                    CommissionerDirectInvite = f.Random.Int(0, 1),
                    VetoAutoPoll = f.Random.Int(0, 1),
                    ReserveAllowDnr = f.Random.Int(0, 1),
                    TaxiAllowVets = f.Random.Int(0, 1),
                    WaiverDayOfWeek = f.Random.Int(0, 6),
                    PlayoffRoundType = f.Random.Int(0, 1),
                    ReserveAllowOut = f.Random.Int(0, 1),
                    ReserveAllowSus = f.Random.Int(0, 1),
                    VetoShowVotes = f.Random.Int(0, 1),
                    TradeDeadline = f.Random.Int(0, 17),
                    TaxiYears = f.Random.Int(1, 10),
                    DailyWaivers = f.Random.Int(0, 1),
                    FaabSuggestions = f.Random.Int(0, 5),
                    DisableTrades = f.Random.Int(0, 1),
                    PickTrading = f.Random.Int(0, 1),
                    Type = f.Random.Int(0, 5),
                    MaxKeepers = f.Random.Int(0, 10),
                    WaiverType = f.Random.Int(0, 1),
                    MaxSubs = f.Random.Int(0, 10),
                    LeagueAverageMatch = f.Random.Int(0, 100),
                    PositionLimitDl = f.Random.Int(0, 4),
                    TradeReviewDays = f.Random.Int(0, 5),
                    BenchLock = f.Random.Int(0, 1),
                    OffseasonAdds = f.Random.Int(0, 5),
                    Leg = f.Random.Int(0, 10),
                    DailyWaiversBase = f.Random.Int(0, 1),
                    ReserveSlots = f.Random.Int(0, 10),
                    ReserveAllowCov = f.Random.Int(0, 1),
                    DailyWaiversLastRan = f.Random.Int(0, 100)
                })
                .RuleFor(l => l.Avatar, f => f.Random.String())
                .RuleFor(l => l.Season, f => f.Date.Between(DateTime.Now.AddYears(-7), DateTime.Now).ToString("yyyy"))
                .RuleFor(l => l.SeasonType, f => f.Random.String())
                .RuleFor(l => l.Sport, f => f.Random.String())
                .RuleFor(l => l.ScoringSettings, f => new ScoringSettings
                {
                    // Passing Settings
                    PassYd = f.Random.Double(0.0, 10.0),
                    PassTd = f.Random.Double(0.0, 10.0),
                    Pass2pt = f.Random.Double(0.0, 5.0),
                    BonusPassYd300 = f.Random.Double(0.0, 10.0),
                    BonusPassYd400 = f.Random.Double(0.0, 10.0),
                    PassInt = f.Random.Double(-5.0, 0.0),

                    // Rushing Settings
                    RushYd = f.Random.Double(0.0, 10.0),
                    RushTd = f.Random.Double(0.0, 10.0),
                    Rush2pt = f.Random.Double(0.0, 5.0),
                    BonusRushYd100 = f.Random.Double(0.0, 10.0),
                    BonusRushYd200 = f.Random.Double(0.0, 10.0),

                    // Receiving Settings
                    Rec = f.Random.Double(0.0, 10.0),
                    RecYd = f.Random.Double(0.0, 10.0),
                    RecTd = f.Random.Double(0.0, 10.0),
                    Rec2pt = f.Random.Double(0.0, 5.0),
                    BonusRecYd100 = f.Random.Double(0.0, 10.0),
                    BonusRecYd200 = f.Random.Double(0.0, 10.0),

                    // IDP Settings
                    IdpInt = f.Random.Double(0.0, 10.0),
                    IdpSafe = f.Random.Double(0.0, 10.0),
                    IdpDefTd = f.Random.Double(0.0, 10.0),
                    IdpFf = f.Random.Double(0.0, 10.0),
                    IdpFumRec = f.Random.Double(0.0, 10.0),
                    IdpTackle = f.Random.Double(0.0, 10.0),
                    IdpTklAst = f.Random.Double(0.0, 10.0),
                    IdpTklSolo = f.Random.Double(0.0, 10.0),
                    IdpTklLoss = f.Random.Double(0.0, 10.0),
                    IdpQbHit = f.Random.Double(0.0, 10.0),
                    IdpSack = f.Random.Double(0.0, 10.0),
                    IdpPassDef = f.Random.Double(0.0, 10.0),
                    IdpPassDef3p = f.Random.Double(0.0, 10.0),
                    IdpBlkKick = f.Random.Double(0.0, 10.0),

                    // Fumble Settings
                    Fum = f.Random.Double(-2.0, 0.0),
                    Ff = f.Random.Double(0.0, 5.0),
                    StFf = f.Random.Double(0.0, 5.0),
                    DefStFf = f.Random.Double(0.0, 5.0),
                    FumLost = f.Random.Double(-2.0, 0.0),
                    FumRec = f.Random.Double(0.0, 5.0),
                    StFumRec = f.Random.Double(0.0, 5.0),
                    DefStFumRec = f.Random.Double(0.0, 5.0),
                    FumRecTd = f.Random.Double(0.0, 10.0),

                    // Defense/Special Teams Settings
                    PtsAllow0 = f.Random.Double(0.0, 10.0),
                    PtsAllow1To6 = f.Random.Double(0.0, 10.0),
                    PtsAllow7To13 = f.Random.Double(0.0, 10.0),
                    PtsAllow14To20 = f.Random.Double(0.0, 10.0),
                    PtsAllow21To27 = f.Random.Double(0.0, 10.0),
                    PtsAllow28To34 = f.Random.Double(0.0, 10.0),
                    PtsAllow35p = f.Random.Double(0.0, 10.0),
                    Int = f.Random.Double(0.0, 10.0),
                    Safe = f.Random.Double(0.0, 10.0),
                    Sack = f.Random.Double(0.0, 10.0),
                    DefTd = f.Random.Double(0.0, 10.0),
                    StTd = f.Random.Double(0.0, 10.0),
                    DefStTd = f.Random.Double(0.0, 10.0),
                    BlkKick = f.Random.Double(0.0, 10.0),
                    BonusTkl10p = f.Random.Double(0.0, 10.0),

                    // Kicker Settings
                    Xpm = f.Random.Double(0.0, 10.0),
                    Xpmiss = f.Random.Double(0.0, -1.0),
                    Fgm0To19 = f.Random.Double(0.0, 5.0),
                    Fgm20To29 = f.Random.Double(0.0, 5.0),
                    Fgm30To39 = f.Random.Double(0.0, 5.0),
                    Fgm40To49 = f.Random.Double(0.0, 5.0),
                    Fgm50p = f.Random.Double(0.0, 5.0),
                    Fgmiss = f.Random.Double(0.0, -1.0)
                })
                .RuleFor(l => l.DraftId, f => f.Random.Guid().ToString())
                .RuleFor(l => l.RosterPositions, f => f.Random.ArrayElements(new[] { "QB", "RB", "WR", "TE", "K", "DEF", "DL", "LB", "DB" }, f.Random.Int(1, 3)).ToArray())
                .RuleFor(l => l.TotalRosters, f => f.Random.Int(10, 30))
                ;

            return leagueFaker.Generate();
        }
    }
}
