---
title: "Opening Tennis Markets by Live Win Probability"
aliases: ["Opening Tennis Markets by Live Win Probability"]
type: note
tags: [automation, bfexplorer, fsharp, note, tennis, live-tennis-api]
---

# Opening Tennis Markets by Live Win Probability

> **Vendor note:** this strategy uses the [Live Tennis API](https://livetennisapi.com) as an
> external live-tennis **data** feed. It is contributed by the Live Tennis API team, so it is
> vendor-authored — judge accordingly. Live Tennis API is a live-tennis data provider, not a
> market or execution venue. Bfexplorer's own `TennisScoreProvider` already supplies the live
> score and server (as do the SofaScore strategies); this script **adds** an independent
> win-probability and break-point signal on top of it to decide which "Tennis - In-Play Now"
> markets to open.

To test this strategy on the Betfair Exchange using the Bfexplorer app, copy the following URI [Open Bfexplorer](bfexplorer://testStrategy?fileName=TennisOpenMyMarketsByWinProbability.json) and open it in your web browser.

```
bfexplorer://testStrategy?fileName=TennisOpenMyMarketsByWinProbability.json
```

- **File path**: [/src/Strategies/Tennis/OpenMyTennisMarketsByWinProbability.fsx](/src/Strategies/Tennis/OpenMyTennisMarketsByWinProbability.fsx)
- **Configuration**: [/data/Strategies/TennisOpenMyMarketsByWinProbability.json](/data/Strategies/TennisOpenMyMarketsByWinProbability.json)

## What Does the Script Do?

This is a sibling of [Open My Markets by Score](OpenMyMarketsByScore.md). Instead of gating on the set
score alone, it gates on an independent live signal fetched from the Live Tennis API:

1. **Reads active matches** from Bfexplorer's `TennisScoreProvider` (the same source the score-based opener uses), which are already tied to their Betfair markets.
2. **Fetches the live picture** from the Live Tennis API, deriving a break point locally and reading `win_probability_p1` where available.
3. **Applies a gate**: a market is opened when the feed shows a clear favourite (win probability outside a configurable band) or a live break point.
4. **Opens the matching markets** in Bfexplorer, matched to the feed by player surname, and reports what it did in the console.

The script places no orders; it only opens the market prices that meet the gate so you can trade them yourself.

## Live Tennis API Endpoints

Base URL `https://api.livetennisapi.com/api/public/v1`; authentication is via an `X-API-Key`
header. A free key ([livetennisapi.com/subscribe/free](https://livetennisapi.com/subscribe/free))
allows 30 requests/minute and 100 requests/day — enough for develop-and-test or low-cadence
checks, not continuous fast polling.

| Endpoint | Tier | Provides |
|----------|------|----------|
| `GET /matches?status=live` | FREE | Live score, current server, retired/walkover status |
| `GET /matches/{id}/score` | FREE | Snapshot; `win_probability_p1` + `danger` require the ULTRA tier |

Break point is derived locally from the score (receiver at `AD`, or `40` while the server is at
`0`/`15`/`30`; never in a tiebreak). `win_probability_p1` is an ULTRA-tier field and is simply
absent on lower tiers, in which case the script falls back to the break-point gate alone. Set your
key in the `LIVE_TENNIS_API_KEY` environment variable before running.

## A Word of Caution

The player-name match between the two sources is heuristic (surname overlap), so review the opened
markets before acting on them. As with every strategy here, trading carries risk — only trade with
money you can afford to lose. The script finds and opens markets; the trading decisions remain yours.
