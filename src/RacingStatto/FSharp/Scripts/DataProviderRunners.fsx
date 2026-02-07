#r "nuget: ExcelProvider"

open System
open FSharp.Interop.Excel

type RacingStattoDataTypes = ExcelFile<"E:/Projects/Bfexplorer/Development/BetfairFramework/BeloSoft.Bfexplorer.HorseRacingStattoBots/Data/RacingStatto.xlsx", "Runners", Range = "D5">

let file = new RacingStattoDataTypes ()

for row in file.Data |> Seq.filter (fun r -> not (String.IsNullOrEmpty r.HORSE) && r.HORSE <> "0" && r.HORSE <> "HORSE") do
    printfn "%s | %d | %0.2f" row.HORSE (int row.``Ov Rank``) row.``Avg Rank``
