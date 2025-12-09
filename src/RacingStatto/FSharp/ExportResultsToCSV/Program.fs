namespace BeloSoft.ExportResultsToCSV

open System.Globalization
open System.IO

open CsvHelper
open CsvHelper.Configuration
open CsvHelper.Configuration.Attributes

open BeloSoft.Data
open BeloSoft.Net

open BeloSoft.Bfexplorer.HorseRacingStatto.API.Models

/// <summary>
/// CsvDataWrapper
/// </summary>
type CsvDataWrapper () =

    let mutable raceRunnerData : RaceRunnerData = nil

    [<Ignore>]
    member _this.RacingStattoData
        with get () = raceRunnerData.RacingStattoData

    member this.GoingRank
        with get () = this.RacingStattoData.GoingRank

    member this.DistanceRank
        with get () = this.RacingStattoData.DistanceRank

    member this.GoingDistRank
        with get () = this.RacingStattoData.GoingDistRank

    member this.CourseRank
        with get () = this.RacingStattoData.CourseRank

    member this.TotalWinsRank
        with get () = this.RacingStattoData.TotalWinsRank

    member this.PercentageTotalRank
        with get () = this.RacingStattoData.PercentageTotalRank

    member this.ORRank
        with get () = this.RacingStattoData.ORRank

    member this.TSRank
        with get () = this.RacingStattoData.TSRank

    member this.RPRRank
        with get () = this.RacingStattoData.RPRRank

    member this.JockeyPercentageRank
        with get () = this.RacingStattoData.JockeyPercentageRank

    member this.TrainerPercentageRank
        with get () = this.RacingStattoData.TrainerPercentageRank

    member this.Rank
        with get () = this.RacingStattoData.Rank

    member this.AverageRank
        with get () = this.RacingStattoData.AverageRank

    member _this.Odds
        with get () = raceRunnerData.SpPrice

    [<BooleanTrueValues "1">]
    [<BooleanFalseValues "0">]
    member _this.IsWinner
        with get () = raceRunnerData.IsWinner

    member this.Profit
        with get () = if raceRunnerData.IsWinner then this.Odds - 1.0 else -1.0

    member _this.SetData data =
        raceRunnerData <- data

/// <summary>
/// CsvDataWrapperMap
/// </summary>
[<Sealed>]
type CsvDataWrapperMap () as this =
    inherit ClassMap<CsvDataWrapper> ()

    do
        this.AutoMap (CultureInfo.InvariantCulture)        

module App =
   
    [<EntryPoint>]
    let main args =
        if args.Length = 1
        then
            printfn "Loading horse racing Statto data."

            let resultRacesResults =
                Http.GetApi<RaceResultData []> "http://localhost:10043/api/getAIAgentDataContextFeedback?dataContextName=RacesResultsForRacingStattoData&forLastResults=1000"
                |> Async.RunSynchronously

            match resultRacesResults with
            | DataResult.Success racesResults ->

                let csvFilePathName, fileExists = 
                    let csvFolder = args.[0]

                    printfn "Saving ML data to the folder: %s." csvFolder

                    let csvFilePathName = Path.Combine [| csvFolder; "MlData.csv" |]

                    csvFilePathName, File.Exists csvFilePathName

                try            
                    use stream = File.Open (csvFilePathName, if fileExists then FileMode.Append else FileMode.Create)
                    use writer = new StreamWriter (stream)
                    use csv = 
                        let config = CsvConfiguration (CultureInfo.InvariantCulture, HasHeaderRecord = not fileExists)

                        new CsvWriter (writer, config) 

                    let csvDataWrapper = CsvDataWrapper ()

                    csv.Context.RegisterClassMap<CsvDataWrapperMap> () |> ignore

                    if not fileExists
                    then
                        csv.WriteHeader<CsvDataWrapper> ()
                        csv.NextRecord ()

                    for raceResult in racesResults do
                        raceResult.Runners 
                        |> Array.iter (fun runner ->                                 
                                csvDataWrapper.SetData runner

                                csv.WriteRecord csvDataWrapper
                                csv.NextRecord ()
                            )
                        
                    printfn "All data has been saved to the CSV file: %s." csvFilePathName
                    0
                with
                | ex -> printfn "Exception: %s" ex.Message; -3

            | DataResult.Failure errorMessage -> printfn "Failed to load API data: %s" errorMessage; -2
        else
            -1
