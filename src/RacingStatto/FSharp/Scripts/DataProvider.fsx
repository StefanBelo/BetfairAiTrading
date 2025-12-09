#r "nuget: ExcelProvider"

open System
open FSharp.Interop.Excel

type RacingStattoDataTypes = ExcelFile<"E:/Data/RacingStatto/021225.xlsx", "Runners">

let file = new RacingStattoDataTypes ()

for row in file.Data |> Seq.filter (fun r -> not (String.IsNullOrEmpty r.HORSE) && r.HORSE <> "0" && r.HORSE <> "HORSE") do
    printfn "%d: %s | %d | %0.2f" (int row.TIME) row.HORSE (int row.``Ov Rank``) row.``Avg Rank``
