// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this Betfair bot.
// It is up to you to determine the level of risk you wish to trade under.
// Only trade with money you can afford to lose.
//
// Vendor note: this script uses the Live Tennis API (https://livetennisapi.com) as an
// EXTERNAL live-tennis data feed. It is contributed by the Live Tennis API team, so it is
// vendor-authored — judge accordingly. Live Tennis API is a live-tennis DATA provider, not a
// market or execution venue. Bfexplorer's own TennisScoreProvider already supplies the live
// score/server; this script ADDS an independent win-probability and break-point signal on top
// of it to gate which "Tennis - In-Play Now" market prices you open.
//
// Endpoints used (base https://api.livetennisapi.com/api/public/v1, auth via the X-API-Key
// header; free key: https://livetennisapi.com/subscribe/free, 30 req/min and 100 req/day):
//   GET /matches?status=live   FREE   live score, current server, retired/walkover status
//   GET /matches/{id}/score    FREE   snapshot; win_probability_p1 + danger require the ULTRA tier
// Break point is derived locally from the score (receiver at AD, or 40 vs server 0/15/30; never
// in a tiebreak). win_probability_p1 is an ULTRA-tier field and is simply absent on lower tiers,
// in which case this script falls back to the break-point gate alone.
//
// Set your key in the LIVE_TENNIS_API_KEY environment variable before running.

module BfexplorerScript

#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.TennisScoreProvider.dll"

open System
open System.Net.Http
open System.Text.Json

open BeloSoft.Data
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.TennisScoreProvider

/// <summary>
/// One live match as seen by the Live Tennis API feed.
/// </summary>
type LiveTennisMatch =
    {
        Player1 : string
        Player2 : string
        WinProbabilityPlayer1 : float option
        HasBreakPoint : bool
    }

[<Literal>]
let LiveTennisApiBaseUrl = "https://api.livetennisapi.com/api/public/v1"

// Open a market when our feed shows a clear favourite (win probability outside this band) or a
// live break point. Tune to taste; these are deliberately conservative defaults.
[<Literal>]
let StrongFavouriteProbability = 0.70

/// <summary>
/// Derive break point from the in-game point strings, per the Live Tennis API rule.
/// </summary>
let deriveBreakPoint (points : string[]) (server : int option) (isTiebreak : bool) =
    match server with
    | Some serverIndex when not isTiebreak && points.Length >= 2 && (serverIndex = 1 || serverIndex = 2) ->
        let serverPoint = points.[serverIndex - 1]
        let receiverPoint = points.[2 - serverIndex]

        if isNull (box serverPoint) || isNull (box receiverPoint)
        then false
        elif receiverPoint = "AD"
        then true
        else receiverPoint = "40" && (serverPoint = "0" || serverPoint = "15" || serverPoint = "30")
    | _ -> false

/// <summary>
/// Parse the Live Tennis API /matches response into simple live-match records.
/// </summary>
let parseLiveMatches (json : string) =
    use document = JsonDocument.Parse json

    let tryGetString (element : JsonElement) (name : string) =
        match element.TryGetProperty name with
        | true, value when value.ValueKind = JsonValueKind.String -> value.GetString ()
        | _ -> String.Empty

    let readMatch (element : JsonElement) =
        let players =
            match element.TryGetProperty "players" with
            | true, value -> value
            | _ -> element

        let player1 =
            match players.TryGetProperty "p1" with
            | true, value -> tryGetString value "name"
            | _ -> String.Empty

        let player2 =
            match players.TryGetProperty "p2" with
            | true, value -> tryGetString value "name"
            | _ -> String.Empty

        let winProbability, hasBreakPoint =
            match element.TryGetProperty "score" with
            | true, score when score.ValueKind = JsonValueKind.Object ->
                let winProbability =
                    match score.TryGetProperty "win_probability_p1" with
                    | true, value when value.ValueKind = JsonValueKind.Number -> Some (value.GetDouble ())
                    | _ -> None

                let server =
                    match score.TryGetProperty "server" with
                    | true, value when value.ValueKind = JsonValueKind.Number -> Some (value.GetInt32 ())
                    | _ -> None

                let isTiebreak =
                    match score.TryGetProperty "is_tiebreak" with
                    | true, value when value.ValueKind = JsonValueKind.True -> true
                    | _ -> false

                let points =
                    match score.TryGetProperty "points" with
                    | true, value when value.ValueKind = JsonValueKind.Array ->
                        value.EnumerateArray ()
                        |> Seq.map (fun p -> if p.ValueKind = JsonValueKind.String then p.GetString () else null)
                        |> Seq.toArray
                    | _ -> Array.empty

                winProbability, deriveBreakPoint points server isTiebreak
            | _ -> None, false

        {
            Player1 = player1
            Player2 = player2
            WinProbabilityPlayer1 = winProbability
            HasBreakPoint = hasBreakPoint
        }

    match document.RootElement.TryGetProperty "data" with
    | true, data when data.ValueKind = JsonValueKind.Array ->
        data.EnumerateArray () |> Seq.map readMatch |> Seq.toList
    | _ -> []

/// <summary>
/// Fetch the current live matches from the Live Tennis API.
/// </summary>
let getLiveTennisMatches (apiKey : string) =
    async {
        use client = new HttpClient ()
        client.DefaultRequestHeaders.Add ("X-API-Key", apiKey)

        let! response =
            client.GetStringAsync (sprintf "%s/matches?status=live" LiveTennisApiBaseUrl)
            |> Async.AwaitTask

        return parseLiveMatches response
    }

/// <summary>
/// Lower-case surname tokens (length >= 3) from a player name, for fuzzy cross-source matching.
/// Betfair names ("Djokovic N.") and Live Tennis API names ("Novak Djokovic") share the surname.
/// </summary>
let surnameTokens (name : string) =
    if String.IsNullOrWhiteSpace name
    then Set.empty
    else
        name.Split ([| ' '; '.'; ','; '-' |], StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (fun token -> token.Trim().ToLowerInvariant ())
        |> Array.filter (fun token -> token.Length >= 3)
        |> Set.ofArray

/// <summary>
/// A live match satisfies the gate when the feed shows a clear favourite or a live break point.
/// </summary>
let passesGate (liveMatch : LiveTennisMatch) =
    let strongFavourite =
        match liveMatch.WinProbabilityPlayer1 with
        | Some probability ->
            probability >= StrongFavouriteProbability || probability <= (1.0 - StrongFavouriteProbability)
        | None -> false

    strongFavourite || liveMatch.HasBreakPoint

/// <summary>
/// Execute
/// </summary>
/// <param name="bfexplorerConsole"></param>
let Execute (bfexplorerConsole : IBfexplorerConsole) =
    let report message =
        bfexplorerConsole.Bfexplorer.OutputMessage message

    let apiKey = Environment.GetEnvironmentVariable "LIVE_TENNIS_API_KEY"

    let tennisScoreProvider = TennisScoreProvider (bfexplorerConsole.BfexplorerService)

    async {
        if String.IsNullOrWhiteSpace apiKey
        then
            do! report "Set your Live Tennis API key in the LIVE_TENNIS_API_KEY environment variable. Free key: https://livetennisapi.com/subscribe/free"
        else
            match! tennisScoreProvider.GetActiveMatches () with
            | DataResult.Success tennisMatches ->
                if tennisMatches.IsEmpty
                then
                    do! report "No tennis matches playing!"
                else
                    match! tennisScoreProvider.UpdateMatches tennisMatches with
                    | Result.Success ->
                        try
                            let! liveMatches = getLiveTennisMatches apiKey

                            let qualifyingMatches = liveMatches |> List.filter passesGate

                            let feedTokens =
                                qualifyingMatches
                                |> List.map (fun liveMatch ->
                                    Set.union (surnameTokens liveMatch.Player1) (surnameTokens liveMatch.Player2)
                                )

                            // A Betfair market matches a qualifying feed match when their player
                            // surnames overlap. Name matching across sources is heuristic.
                            let marketMatchesFeed (market : Market) =
                                let marketTokens =
                                    market.Selections
                                    |> Seq.map (fun selection -> surnameTokens selection.Name)
                                    |> Seq.fold Set.union Set.empty

                                feedTokens
                                |> List.exists (fun tokens -> not (Set.isEmpty (Set.intersect tokens marketTokens)))

                            let marketsToOpen =
                                tennisMatches
                                |> List.map (fun tennisMatch -> tennisMatch.Market)
                                |> List.filter marketMatchesFeed

                            if marketsToOpen.IsEmpty
                            then
                                do! report "No live matches met the win-probability / break-point gate."
                            else
                                do! report (sprintf "Opening %d market(s) gated by the Live Tennis API feed." marketsToOpen.Length)

                                bfexplorerConsole.OpenMyMarkets marketsToOpen
                        with
                        | error -> do! report (sprintf "Live Tennis API request failed: %s" error.Message)

                    | Result.Failure errorMessage -> do! report errorMessage

            | DataResult.Failure errorMessage -> do! report errorMessage
    }
    |> Async.RunSynchronously
