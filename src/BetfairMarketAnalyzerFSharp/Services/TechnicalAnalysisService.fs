namespace BetfairMarketAnalyzerFSharp.Services

open System
open System.Linq
open Skender.Stock.Indicators
open BetfairMarketAnalyzerFSharp.Models

module TechnicalAnalysisService =
    
    let private convertToQuotes (priceData: TradedPriceVolume list) =
        priceData
        |> List.groupBy (fun x -> DateTime(x.Time.Year, x.Time.Month, x.Time.Day, x.Time.Hour, x.Time.Minute, 0))
        |> List.sortBy fst
        |> List.map (fun (groupKey, trades) ->
            let orderedTrades = trades |> List.sortBy (_.Time)
            match orderedTrades with
            | [] -> None
            | _ ->
                Some {
                    Date = groupKey
                    Open = (List.head orderedTrades).Price
                    High = orderedTrades |> List.map (_.Price) |> List.max
                    Low = orderedTrades |> List.map (_.Price) |> List.min
                    Close = (List.last orderedTrades).Price
                    Volume = orderedTrades |> List.sumBy (_.Volume)
                })
        |> List.choose id

    let private determineMomentum (quotes: BetfairQuote list) =
        if quotes.Length < 5 then "Insufficient Data"
        else
            let recent = quotes |> List.rev |> List.truncate 5 |> List.map (_.Close)
            if recent.Length < 2 then "Insufficient Data"
            else
                let avg1 = recent |> List.truncate 2 |> List.average
                let avg2 = recent |> List.skip (min 3 recent.Length) |> List.average
                if avg1 > avg2 then "Bullish"
                elif avg1 < avg2 then "Bearish"
                else "Neutral"

    let private calculateTechnicalIndicators (quotes: BetfairQuote list) =
        if quotes.Length < 14 then
            {
                RSI = None
                SMA5 = None
                SMA10 = None
                EMA5 = None
                EMA10 = None
                MACD = None
                MACDSignal = None
                BollingerUpper = None
                BollingerLower = None
                BollingerMiddle = None
                MomentumDirection = "Insufficient Data"
                VolumeMovingAverage = 0m
            }
        else
            try
                let quotesSeq = quotes |> Seq.cast<IQuote>
                let rsi = quotesSeq.GetRsi(14).LastOrDefault()
                let sma5 = quotesSeq.GetSma(5).LastOrDefault()
                let sma10 = quotesSeq.GetSma(10).LastOrDefault()
                let ema5 = quotesSeq.GetEma(5).LastOrDefault()
                let ema10 = quotesSeq.GetEma(10).LastOrDefault()
                let macd = quotesSeq.GetMacd().LastOrDefault()
                let bollinger = quotesSeq.GetBollingerBands().LastOrDefault()
                
                let volumeAvg = 
                    quotes 
                    |> List.rev 
                    |> List.take (min 10 quotes.Length)
                    |> List.averageBy (_.Volume)

                {
                    RSI = if rsi <> null && rsi.Rsi.HasValue then Some (decimal rsi.Rsi.Value) else None
                    SMA5 = if sma5 <> null && sma5.Sma.HasValue then Some (decimal sma5.Sma.Value) else None
                    SMA10 = if sma10 <> null && sma10.Sma.HasValue then Some (decimal sma10.Sma.Value) else None
                    EMA5 = if ema5 <> null && ema5.Ema.HasValue then Some (decimal ema5.Ema.Value) else None
                    EMA10 = if ema10 <> null && ema10.Ema.HasValue then Some (decimal ema10.Ema.Value) else None
                    MACD = if macd <> null && macd.Macd.HasValue then Some (decimal macd.Macd.Value) else None
                    MACDSignal = if macd <> null && macd.Signal.HasValue then Some (decimal macd.Signal.Value) else None
                    BollingerUpper = if bollinger <> null && bollinger.UpperBand.HasValue then Some (decimal bollinger.UpperBand.Value) else None
                    BollingerLower = if bollinger <> null && bollinger.LowerBand.HasValue then Some (decimal bollinger.LowerBand.Value) else None
                    BollingerMiddle = if bollinger <> null && bollinger.Sma.HasValue then Some (decimal bollinger.Sma.Value) else None
                    MomentumDirection = determineMomentum quotes
                    VolumeMovingAverage = volumeAvg
                }
            with
            | _ -> 
                {
                    RSI = None
                    SMA5 = None
                    SMA10 = None
                    EMA5 = None
                    EMA10 = None
                    MACD = None
                    MACDSignal = None
                    BollingerUpper = None
                    BollingerLower = None
                    BollingerMiddle = None
                    MomentumDirection = "Error"
                    VolumeMovingAverage = 0m
                }

    let private determineTrendDirection (quotes: BetfairQuote list) =
        if quotes.Length < 2 then "Insufficient Data"
        else
            let recentQuotes = quotes |> List.rev |> List.take (min 10 quotes.Length)
            let priceChange = (List.head recentQuotes).Close - (List.last recentQuotes).Close

            if priceChange < -0.05m then "Shortening (Backing Pressure)"
            elif priceChange > 0.05m then "Lengthening (Laying Pressure)"
            else "Sideways"

    let private analyzeSupportResistance (priceData: TradedPriceVolume list) =
        let priceGroups = 
            priceData
            |> List.groupBy (fun x -> Math.Round(x.Price, 2))
            |> List.filter (fun (_, group) -> group.Length >= 3)
            |> List.sortByDescending (fun (_, group) -> group |> List.sumBy (_.Volume))
            |> List.truncate 10

        let currentPrice = (List.last priceData).Price

        let supportLevels, resistanceLevels =
            priceGroups
            |> List.map (fun (level, group) ->
                {
                    Level = level
                    TouchCount = group.Length
                    TotalVolume = group |> List.sumBy (_.Volume)
                    LastTouch = group |> List.map (_.Time) |> List.max
                    Strength = if (group |> List.sumBy (_.Volume)) > 100m then "Strong" else "Moderate"
                })
            |> List.partition (fun level -> level.Level < currentPrice)

        let sortedSupport = supportLevels |> List.sortByDescending (_.Level) |> List.truncate 5
        let sortedResistance = resistanceLevels |> List.sortBy (_.Level) |> List.truncate 5

        (sortedSupport, sortedResistance)

    let private analyzeVolume (priceData: TradedPriceVolume list) =
        let avgVolume = priceData |> List.averageBy (_.Volume)
        let volumeThreshold = avgVolume * 2m

        priceData
        |> List.mapi (fun i current ->
            if i = 0 then None
            else
                let previous = priceData.[i - 1]
                if current.Volume > volumeThreshold then
                    let direction = if current.Price < previous.Price then "Backing" else "Laying"
                    let volumeType = if current.Volume > avgVolume * 5m then "SteamMove" else "Spike"
                    
                    Some {
                        Time = current.Time
                        Price = current.Price
                        Volume = current.Volume
                        Type = volumeType
                        Direction = direction
                    }
                else None)
        |> List.choose id
        |> List.sortByDescending (_.Volume)
        |> List.truncate 10

    let private analyzeMarketFlow (priceData: TradedPriceVolume list) =
        let recentData = priceData |> List.rev |> List.take (min 50 priceData.Length)
        
        if recentData.Length < 2 then
            {
                DominantDirection = "Neutral"
                BackingPressure = 50m
                LayingPressure = 50m
                RecentVolumeRatio = 1m
                SteamMoveDetected = false
                LastSteamMove = None
                MarketSentiment = "Insufficient Data"
            }
        else
            let mutable backingVolume = 0m
            let mutable layingVolume = 0m

            for i in 1 .. recentData.Length - 1 do
                let current = recentData.[i]
                let previous = recentData.[i - 1]

                if current.Price < previous.Price then
                    backingVolume <- backingVolume + current.Volume
                elif current.Price > previous.Price then
                    layingVolume <- layingVolume + current.Volume

            let totalVolume = backingVolume + layingVolume
            let backingPressure = if totalVolume > 0m then (backingVolume / totalVolume) * 100m else 50m
            let layingPressure = 100m - backingPressure

            let avgVolume = recentData |> List.averageBy (_.Volume)
            let steamMove = recentData |> List.exists (fun x -> x.Volume > avgVolume * 5m)
            let lastSteamMove = 
                if steamMove then
                    recentData 
                    |> List.filter (fun x -> x.Volume > avgVolume * 5m)
                    |> List.sortByDescending (_.Time)
                    |> List.tryHead
                    |> Option.map (_.Time)
                else None

            let dominantDirection =
                if backingPressure > 60m then "Backing"
                elif layingPressure > 60m then "Laying"
                else "Neutral"

            let marketSentiment =
                if backingPressure > 70m then "Strong Backing"
                elif layingPressure > 70m then "Strong Laying"
                else "Balanced"

            {
                DominantDirection = dominantDirection
                BackingPressure = Math.Round(backingPressure, 1)
                LayingPressure = Math.Round(layingPressure, 1)
                RecentVolumeRatio = Math.Round(backingVolume / Math.Max(layingVolume, 1m), 2)
                SteamMoveDetected = steamMove
                LastSteamMove = lastSteamMove
                MarketSentiment = marketSentiment
            }

    let private calculateBackToLayProfit (backPrice: decimal) (layPrice: decimal) =
        if layPrice >= backPrice then 0m
        else Math.Round(((backPrice - layPrice) / backPrice) * 100m, 2)

    let private calculateLayToBackProfit (layPrice: decimal) (backPrice: decimal) =
        if backPrice <= layPrice then 0m
        else Math.Round(((backPrice - layPrice) / layPrice) * 100m, 2)

    let private generateTradingRecommendations 
        (priceData: TradedPriceVolume list) 
        (indicators: TechnicalIndicators)
        (supportLevels: SupportResistanceLevel list, resistanceLevels: SupportResistanceLevel list)
        (marketFlow: MarketFlow) =
        
        let recommendations = ResizeArray<TradingRecommendation>()
        let currentPrice = (List.last priceData).Price
        let recentTrend = priceData |> List.rev |> List.take (min 10 priceData.Length)
        let priceDirection = (List.head recentTrend).Price - (List.last recentTrend).Price

        // Back-to-Lay Strategy (Primary)
        if marketFlow.DominantDirection = "Backing" && priceDirection < 0m then
            let nearestSupport = supportLevels |> List.tryHead
            let targetPrice = 
                match nearestSupport with
                | Some support -> support.Level
                | None -> currentPrice * 0.95m

            recommendations.Add({
                Strategy = "Back-to-Lay Trading"
                Action = "Back-to-Lay"
                EntryPrice = currentPrice
                TargetPrice = targetPrice
                StopLoss = currentPrice * 1.05m
                ExpectedProfit = calculateBackToLayProfit currentPrice targetPrice
                RiskReward = 2.5m
                TimeFrame = "5-15 minutes"
                RiskLevel = "Moderate"
                ConfidenceLevel = 8
                Logic = "Strong backing pressure with volume confirmation supporting further shortening"
                PositionSizing = 5m
            })

        // Pure Backing Strategy
        if marketFlow.BackingPressure > 65m && 
           (indicators.RSI |> Option.map (fun rsi -> rsi < 70m) |> Option.defaultValue true) then
            recommendations.Add({
                Strategy = "Pure Backing Position"
                Action = "Back"
                EntryPrice = currentPrice
                TargetPrice = 0m // Hold to win
                StopLoss = currentPrice * 1.15m
                ExpectedProfit = (1m / currentPrice - 1m) * 100m // Win percentage
                RiskReward = currentPrice
                TimeFrame = "Hold to race"
                RiskLevel = "Aggressive"
                ConfidenceLevel = 7
                Logic = "Heavy backing pressure indicates market confidence"
                PositionSizing = 2m
            })

        // Lay-to-Back Strategy (Counter-trend)
        if marketFlow.DominantDirection = "Laying" && priceDirection > 0m then
            let nearestResistance = resistanceLevels |> List.tryHead
            let targetPrice = 
                match nearestResistance with
                | Some resistance -> resistance.Level
                | None -> currentPrice * 1.05m

            recommendations.Add({
                Strategy = "Lay-to-Back Trading"
                Action = "Lay-to-Back"
                EntryPrice = currentPrice
                TargetPrice = targetPrice
                StopLoss = currentPrice * 0.95m
                ExpectedProfit = calculateLayToBackProfit currentPrice targetPrice
                RiskReward = 1.8m
                TimeFrame = "10-20 minutes"
                RiskLevel = "Conservative"
                ConfidenceLevel = 6
                Logic = "Laying pressure may push odds higher providing lay-to-back opportunity"
                PositionSizing = 3m
            })

        // Scalping Strategy
        if recentTrend.Length > 5 && indicators.VolumeMovingAverage > 10m then
            recommendations.Add({
                Strategy = "Scalping"
                Action = "Quick Trade"
                EntryPrice = currentPrice
                TargetPrice = currentPrice + (if priceDirection > 0m then 0.02m else -0.02m)
                StopLoss = currentPrice + (if priceDirection > 0m then -0.03m else 0.03m)
                ExpectedProfit = 2m // 2 ticks
                RiskReward = 0.67m
                TimeFrame = "1-3 minutes"
                RiskLevel = "Moderate"
                ConfidenceLevel = 5
                Logic = "High volume and volatility suitable for quick scalping"
                PositionSizing = 8m
            })

        recommendations
        |> Seq.toList
        |> List.sortByDescending (_.ConfidenceLevel)

    let analyzeSelection (marketData: MarketSelectionData) =
        if marketData.Selections.IsEmpty then
            invalidArg "marketData" "No selection data provided"

        let selection = List.head marketData.Selections
        let priceData = selection.TradedPricesAndVolume |> List.sortBy (_.Time)

        if priceData.IsEmpty then
            invalidArg "priceData" "No price data available"

        // Convert to OHLCV format for technical analysis
        let quotes = convertToQuotes priceData
        
        // Calculate technical indicators
        let indicators = calculateTechnicalIndicators quotes
        
        // Analyze support/resistance levels
        let supportLevels, resistanceLevels = analyzeSupportResistance priceData
        
        // Analyze volume patterns
        let volumeAnalysis = analyzeVolume priceData
        
        // Detect market flow
        let marketFlow = analyzeMarketFlow priceData
        
        // Generate trading recommendations
        let recommendations = generateTradingRecommendations priceData indicators (supportLevels, resistanceLevels) marketFlow

        let currentPrice = (List.last priceData).Price
        let openingPrice = (List.head priceData).Price
        let oddsMovement = currentPrice - openingPrice
        let oddsMovementPercentage = (oddsMovement / openingPrice) * 100m

        {
            SelectionName = selection.Selection.Name
            CurrentOdds = currentPrice
            OpeningOdds = openingPrice
            ImpliedProbability = Math.Round(1m / currentPrice * 100m, 2)
            OddsMovement = Math.Round(oddsMovement, 2)
            OddsMovementPercentage = Math.Round(oddsMovementPercentage, 2)
            TrendDirection = determineTrendDirection quotes
            TotalVolume = priceData |> List.sumBy (_.Volume)
            SupportLevels = supportLevels
            ResistanceLevels = resistanceLevels
            VolumeSpikes = volumeAnalysis
            Indicators = indicators
            TradingRecommendations = recommendations
            MarketFlow = marketFlow
            AnalysisTime = DateTime.Now
            TimeToRaceStart = marketData.Market.StartTime - DateTime.Now
        }
