// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module HorseRacingBotTrigger

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net9.0-windows\"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

open System

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

let getRaceDistance (market : Market) =
    let raceDistanceString, inMiles = 
        let marketInfo = market.MarketInfo
        let data = marketInfo.MarketName.Split ' '

        match marketInfo.BetEvent.CountryCode with
        | "GB" | "IE" -> data.[0], true
        | "US" -> data.[1], true
        | "FR" -> data.[0], false
        | _ -> data.[1], false
            
    if inMiles
    then
        let distanceParts = raceDistanceString.Length / 2

        let mutable distance = 0.0
        let mutable index = 0

        while index <= distanceParts do
            distance <- distance + (
                    float ((int)raceDistanceString.[index] - (int)'0') * 
                    match raceDistanceString.Chars(index + 1) with
                    | 'm' -> 1609.344
                    | _ -> 201.168
                )

            index <- index + 2

        distance
    else
        float (raceDistanceString.TrimEnd 'm')

let getRaceDistanceOpt (market: Market) =
    let raceDistanceString, inMiles =
        let marketInfo = market.MarketInfo
        let data = marketInfo.MarketName.Split ' '

        match marketInfo.BetEvent.CountryCode with
        | "GB" | "IE" -> data.[0], true
        | "US" -> data.[1], true
        | "FR" -> data.[0], false
        | _ -> data.[1], false

    if inMiles then
        let rec parseDistance acc i =
            if i >= raceDistanceString.Length then acc
            else
                let digitChar = raceDistanceString.[i]
                let unitChar = raceDistanceString.[i + 1]

                if Char.IsDigit digitChar && (unitChar = 'm' || unitChar = 'f') 
                then
                    let digit = float (int digitChar - int '0')
                    let factor = if unitChar = 'm' then 1609.344 else 201.168
                    
                    parseDistance (acc + digit * factor) (i + 2)
                else
                    failwithf "Invalid distance format: %s" raceDistanceString

        parseDistance 0.0 0
    else
        let trimmed = raceDistanceString.TrimEnd 'm'

        match Double.TryParse trimmed with
        | true, value -> value
        | _ -> failwithf "Invalid distance format: %s" raceDistanceString

let getFavourite (market : Market) =
    getFavouriteSelections market |> List.head

/// <summary>
/// HorseRacingRaceDistanceReopenBotTrigger
/// </summary>
type HorseRacingRaceDistanceBotTrigger (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let isMyHorseRacingMarket () =
        market.MarketInfo.BetEventType.Id = 7 && market.MarketDescription.MarketType = "WIN"

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member this.Execute () =
            if isMyHorseRacingMarket ()
            then
                let allowedDistance = defaultArg (botTriggerParameters.GetParameter<float> "AllowedDistance") 1609.344
                let raceDistance = getRaceDistance market

                this.Report (sprintf "Race distance: %.2f" raceDistance)

                if raceDistance >= allowedDistance
                then
                    TriggerResult.ExecuteActionBotOnSelection (getFavourite market)
                else
                    TriggerResult.EndExecution
            else
                TriggerResult.EndExecutionWithMessage "You can run this bot on a horse racing market only!"

        /// <summary>
        /// EndExecution
        /// </summary>
        member _this.EndExecution () =
            ()
