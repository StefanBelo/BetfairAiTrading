#r "nuget: Plotly.NET"
open Plotly.NET

let xs = [1; 2; 3; 4; 5]
let ys = [10.0; 15.0; 13.0; 17.0; 19.0]
let pointLabels = [ "Start"; "Up"; "Dip"; "Gain"; "Finish" ]

let chart =
    Chart.Line(
        xs,
        ys,
        Name = "Series 1",
        MultiText = pointLabels,           // Labels per point
        ShowMarkers = true,
        TextPosition = StyleParam.TextPosition.TopCenter
    )
    |> Chart.withTitle("Line chart with point labels")

Chart.show chart