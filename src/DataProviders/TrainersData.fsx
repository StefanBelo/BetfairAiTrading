#r "nuget: FSharp.Data"

open FSharp.Data

[<Literal>]
let TrainersUrl = "https://crate.horseracing.software/tom/functions/api.getTrainers.php"

type Trainers = JsonProvider<TrainersUrl>
type TrainersCsv  = CsvProvider<Schema="Name (string), Postcode (string)", HasHeaders = false>

let allTrainers = Trainers.Load TrainersUrl

let csv =
    allTrainers.Data
    |> Seq.map (fun trainer -> TrainersCsv.Row (trainer.Name, defaultArg trainer.Postcode ""))
    |> Seq.toList
    |> fun rows -> new TrainersCsv (rows)

csv.Save ("Data/Trainers.csv")
