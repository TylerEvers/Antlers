# Antlers

## DraftKings NFL markets

`DraftKingsApiClient.GetLeagueSubcategoryMarkets(88808, 4518)` retrieves an NFL subcategory's markets and associated events from DraftKings. The result exposes market selections, participant IDs/names, odds, line/points, milestone values, and the exact raw payload. The US-IA NFL page identified subcategory `4518` as Game Lines and `16569` as Player Props on 2026-11-05. Pass another subcategory ID and, when needed, jurisdiction to retrieve a different market group.

## Bovada NFL odds

`BovadaApiClient.GetNflOddsAsync()` retrieves the NFL event feed from Bovada. Its response flattens events, markets, and outcomes into `Events`, `Markets`, and `Selections` collections, including UTC start times, market periods, participant names, handicap lines, American and decimal odds, suspension status, and provider metadata. `RawPayloadJson` preserves the source response for Mothership syncs.