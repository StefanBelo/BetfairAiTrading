#r "nuget: ExcelProvider"

open System
open FSharp.Interop.Excel

type RacingStattoDataTypes = ExcelFile<"E:/Data/RacingStatto/230126.xlsx", "DataCombos", Range = "D6">

let file = new RacingStattoDataTypes ()

for row in file.Data |> Seq.filter (fun r -> not (String.IsNullOrEmpty r.Horse)) do
    printfn "%d: %s | %d | %0.2f" (int row.Time) row.Horse (int row.``Ov Rank``) row.``% ROI``
