#r "nuget: ExcelProvider"

open System
open FSharp.Interop.Excel

type RacingStattoDataTypes = ExcelFile<"E:/Data/RacingStatto/Data.xlsx", "Runners">

let file = new RacingStattoDataTypes ()

for row in file.Data |> Seq.filter (fun r -> r.Date <> DateTime.MinValue) do
    printfn "%s: %s | %d | %d | %d | %0.2f" (string row.TIME) row.HORSE (int row.``Ov Rank``) (int row.AVGMPH) (int row.``Fast GD``) row.``Avg Rank``