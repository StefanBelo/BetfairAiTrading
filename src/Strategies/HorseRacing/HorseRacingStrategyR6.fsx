(*
Horse Racing Strategy R6 Implementation
Silent execution mode - no reporting/output
*)

open System
open System.Collections.Generic

// Data context types
type RacingPostData = {
    PredictionScore: float
    ProfessionalAnalysis: string
}

type BaseFormData = {
    OfficialRating: float
    Form: string
    ForecastPrice: float
}

type MarketData = {
    CurrentPrice: float
    MinPrice: float
    MaxPrice: float
    Volume: float
    ProbabilityMovement: string // "Shortening", "Stable", "Drifting"
    HistoricalSuccess: string   // "Strong", "Avg", "Weak"
    Stability: float
}

type HorseData = {
    SelectionId: string
    RacingPost: RacingPostData
    BaseForm: BaseFormData
    Market: MarketData
    LastRacesDescription: string
}

[<AutoOpen>]
module BfexplorerApp =
    type DataContext = {
        SelectionId: string
        RacingPostData: RacingPostData
        BaseFormData: BaseFormData
        MarketData: MarketData
        LastRacesDescription: string
    }

    type MarketInfo = {
        MarketId: string
        Volume: float
    }

    let GetAllDataContextForBetfairMarket (contextNames: string[], marketId: string) = 
        // Implementation would call MCP server
        [||]

    let ExecuteBfexplorerStrategySettings (strategyName: string, marketId: string, selectionId: string) =
        // Implementation would call MCP server
        ()

    let GetActiveBetfairMarket () =
        // Implementation would call MCP server
        { MarketId = ""; Volume = 0.0 }

// Calculation functions
let calculateSemanticFormScore (description:string) =
    // TODO: Implement NLP analysis of race descriptions
    // Placeholder - returns random score for now
    Random().Next(50, 100) |> float

let calculateValueScore currentPrice forecastPrice =
    ((currentPrice - forecastPrice) / forecastPrice) * 100.0

let calculateStabilityScore minPrice maxPrice =
    100.0 - ((maxPrice - minPrice) / minPrice * 100.0)

let calculateCompositeScore (horse:HorseData) =
    let semanticForm = calculateSemanticFormScore horse.LastRacesDescription * 0.35
    let value = calculateValueScore horse.Market.CurrentPrice horse.BaseForm.ForecastPrice * 0.25
    let prediction = horse.RacingPost.PredictionScore * 0.25
    let stability = calculateStabilityScore horse.Market.MinPrice horse.Market.MaxPrice * 0.15
    semanticForm + value + prediction + stability

let calculateCWAF (market:MarketData) =
    let marketConfidence = 
        match market.Stability with
        | s when s > 90.0 -> 1.2
        | s when s > 80.0 -> 1.1
        | s when s > 70.0 -> 1.0
        | _ -> 0.9

    let probMovement = 
        match market.ProbabilityMovement with
        | "Shortening" -> 1.2
        | "Stable" -> 1.0
        | _ -> 0.8

    let volume = 
        match market.Volume with
        | v when v > 5000.0 -> 1.1
        | v when v > 2000.0 -> 1.0
        | _ -> 0.9

    let historical = 
        match market.HistoricalSuccess with
        | "Strong" -> 1.1
        | "Avg" -> 1.0
        | _ -> 0.9

    marketConfidence * probMovement * volume * historical

let calculateEnhancedEV (horse:HorseData) =
    let winProbability = ((calculateSemanticFormScore horse.LastRacesDescription * 0.6) + (horse.RacingPost.PredictionScore * 0.4)) / 100.0
    let baseEV = (winProbability * (horse.Market.CurrentPrice - 1.0)) - ((1.0 - winProbability) * 1.0)
    baseEV * (calculateCWAF horse.Market)

// Main execution logic
let executeStrategy (marketId:string) =
    // Get required data contexts
    let dataContexts = BfexplorerApp.GetAllDataContextForBetfairMarket([|"RacingpostDataForHorsesInfo"; "HorsesBaseBetfairFormData"; "MarketSelectionsTradedPricesData"|], marketId)
    
    // Transform data into HorseData records
    let horses = dataContexts |> Array.map (fun ctx ->
        {
            SelectionId = ctx.SelectionId
            RacingPost = {
                PredictionScore = ctx.RacingPostData.PredictionScore
                ProfessionalAnalysis = ctx.RacingPostData.ProfessionalAnalysis
            }
            BaseForm = {
                OfficialRating = ctx.BaseFormData.OfficialRating
                Form = ctx.BaseFormData.Form
                ForecastPrice = ctx.BaseFormData.ForecastPrice
            }
            Market = {
                CurrentPrice = ctx.MarketData.CurrentPrice
                MinPrice = ctx.MarketData.MinPrice
                MaxPrice = ctx.MarketData.MaxPrice
                Volume = ctx.MarketData.Volume
                ProbabilityMovement = ctx.MarketData.ProbabilityMovement
                HistoricalSuccess = ctx.MarketData.HistoricalSuccess
                Stability = calculateStabilityScore ctx.MarketData.MinPrice ctx.MarketData.MaxPrice
            }
            LastRacesDescription = ctx.LastRacesDescription
        })

    // Calculate scores and EVs
    let analyzedHorses = horses |> Array.map (fun h ->
        let composite = calculateCompositeScore h
        let enhancedEV = calculateEnhancedEV h
        (h, composite, enhancedEV))

    // Find favorite (lowest price)
    let favorite = analyzedHorses |> Array.minBy (fun (h,_,_) -> h.Market.CurrentPrice)

    // Check lay conditions on favorite
    let (favHorse, favComposite, favEV) = favorite
    if favHorse.Market.CurrentPrice <= 2.0 && favEV < 0.0 && 
       calculateSemanticFormScore favHorse.LastRacesDescription < 70.0 && 
       favComposite < 55.0 &&
       (analyzedHorses |> Array.filter (fun (_,_,ev) -> ev > 0.0) |> Array.length >= 2) then
        BfexplorerApp.ExecuteBfexplorerStrategySettings("Lay 10 Euro", marketId, favHorse.SelectionId)
    else
        // Check back conditions on all horses
        let backCandidates = analyzedHorses |> Array.filter (fun (h:HorseData,comp:float,ev:float) ->
            calculateSemanticFormScore h.LastRacesDescription >= 70.0 &&
            comp >= 60.0 &&
            ev >= 0.05 &&
            h.Market.CurrentPrice >= 2.0 && h.Market.CurrentPrice <= 15.0)

        if backCandidates.Length > 0 then
            let bestCandidate = 
                let maxEV = backCandidates |> Array.map (fun (_,_,ev) -> ev) |> Array.max
                let closeEVs = backCandidates |> Array.filter (fun (_,_,ev) -> ev >= maxEV - 0.05)
                if closeEVs.Length > 1 then
                    closeEVs |> Array.maxBy (fun (_,comp,_) -> comp)
                else
                    backCandidates |> Array.maxBy (fun (_,_,ev) -> ev)

            let (bestHorse, _, _) = bestCandidate
            BfexplorerApp.ExecuteBfexplorerStrategySettings("Bet 10 Euro", marketId, bestHorse.SelectionId)

// Main entry point
let market = BfexplorerApp.GetActiveBetfairMarket()
if market.Volume > 1000.0 then
    executeStrategy market.MarketId
