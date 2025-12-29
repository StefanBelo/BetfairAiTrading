# Favourite Selection Plotly Chart Script

## Objective
Update the script to retrieve price history data for the favourite selection in the active market and generate an interactive Plotly chart displaying price movement over time.

## Current Implementation

### 1. Assembly References (Already Added)
```fsharp
#r "BeloSoft.Betfair.WebAPI.Selenium.dll"
open BeloSoft.Bfexplorer.Domain.Data
open BeloSoft.Betfair.WebAPI.Selenium.Models
```

### 2. Data Retrieval Pattern (Already Implemented)
The script uses the following approach:
```fsharp
// Get data context from Bfexplorer service
match! bfexplorerConsole.Bfexplorer.GetDataContextForMarketSelection (
    [| "MarketSelectionsPriceHistoryData" |], 
    market, 
    selection
) with
| DataResult.Success marketDataContext -> 
    let selectionData = marketDataContext.SelectionsData.Head
    
    // Extract timePriceVolumes from SelectionDataContext.Data dictionary
    match mapToTimePriceVolumes selectionData with
    | Some timePriceVolumes -> 
        // Process and chart the data
    | None -> 
        // Failed to extract data
```

### 3. Data Structure
The `TimePriceVolume` type (from `BeloSoft.Betfair.WebAPI.Selenium.Models`) contains:
- `Time` : DateTime - timestamp of the price point
- `Price` : Double - decimal odds at that time
- `Volume` : Double - matched volume at that price

**Note:** The data is retrieved as `TimePriceVolume array` from the `SelectionDataContext.Data` dictionary using key `"timePriceVolumes"`.

## Next Steps: Generate Plotly Chart

### 1. Add Plotly.NET NuGet Package
Add reference to Plotly.NET:
```fsharp
#r "nuget: Plotly.NET, 5.0.0"
#r "nuget: Plotly.NET.ImageExport, 5.0.0"
```

Open the namespace:
```fsharp
open Plotly.NET
open Plotly.NET.LayoutObjects
```

### 2. Transform Data for Plotly
Create chart data from `TimePriceVolume array`:
```fsharp
// Extract times and prices
let times = timePriceVolumes |> Array.map (fun tpv -> tpv.Time)
let prices = timePriceVolumes |> Array.map (fun tpv -> tpv.Price)
let volumes = timePriceVolumes |> Array.map (fun tpv -> tpv.Volume)
```

### 3. Create Plotly Chart
Generate a line chart with scatter markers:
```fsharp
let chart =
    Chart.Line(
        x = times,
        y = prices,
        Name = "Price",
        MarkerColor = Color.fromKeyword Blue
    )
    |> Chart.withMarkerStyle(Size = 6)
    |> Chart.withXAxisStyle(
        Title = Title.init("Time"),
        Showgrid = true
    )
    |> Chart.withYAxisStyle(
        Title = Title.init("Price (Decimal Odds)"),
        Showgrid = true
    )
    |> Chart.withTitle(
        Title.init($"{selection.Name} - {market.MarketFullName}")
    )
    |> Chart.withSize(1200, 600)
```

### 4. Optional: Add Volume as Marker Size
Scale volume to appropriate marker sizes:
```fsharp
// Normalize volumes for marker sizes (range 5-20)
let maxVolume = volumes |> Array.max
let markerSizes = 
    volumes 
    |> Array.map (fun v -> 5.0 + (v / maxVolume * 15.0))

let chartWithVolume =
    Chart.Scatter(
        x = times,
        y = prices,
        mode = StyleParam.Mode.Lines_Markers,
        Name = "Price",
        MarkerColor = Color.fromKeyword Blue,
        MarkerSize = markerSizes
    )
    |> Chart.withXAxisStyle(Title = Title.init("Time"))
    |> Chart.withYAxisStyle(Title = Title.init("Price (Decimal Odds)"))
    |> Chart.withTitle($"{selection.Name} - Price Movement")
```

### 5. Save Chart to HTML File
Generate timestamped filename and save:
```fsharp
let timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss")
let fileName = $"favourite_chart_{timestamp}.html"
let filePath = System.IO.Path.Combine(@"E:\Projects\BetfairAiTrading\charts", fileName)

// Ensure directory exists
System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath)) |> ignore

// Save chart
chart |> Chart.show  // Opens in browser
chart |> Chart.saveSVG(filePath.Replace(".html", ".svg"))  // Save as SVG
```

### 6. Alternative: Display in Bfexplorer
If you want to display inline without saving:
```fsharp
// Generate HTML string
let htmlContent = chart |> GenericChart.toChartHTML

// Report the chart location or display
do! report $"Chart generated with {timePriceVolumes.Length} data points"
do! report $"Price range: {prices |> Array.min} - {prices |> Array.max}"
do! report $"Time range: {times |> Array.head} to {times |> Array.last}"
```

## Implementation Plan

Update the existing code in the `Some timePriceVolumes ->` branch:

1. **Extract and prepare data**
   - Get times, prices, volumes arrays
   - Calculate statistics (min, max, count)

2. **Create the Plotly chart**
   - Use `Chart.Line` or `Chart.Scatter`
   - Add appropriate styling and labels
   - Include market and selection info in title

3. **Save and report**
   - Save to timestamped HTML file
   - Report success with data statistics
   - Optionally open in browser

4. **Error handling**
   - Handle empty arrays
   - Handle file I/O errors
   - Report meaningful error messages

## Expected Output

```fsharp
match mapToTimePriceVolumes selectionData with
| Some timePriceVolumes when timePriceVolumes.Length > 0 -> 
    // Extract data
    let times = timePriceVolumes |> Array.map (fun tpv -> tpv.Time)
    let prices = timePriceVolumes |> Array.map (fun tpv -> tpv.Price)
    
    // Create chart
    let chart =
        Chart.Line(times, prices)
        |> Chart.withTitle($"{selection.Name} Price Movement")
        |> Chart.withXAxisStyle("Time")
        |> Chart.withYAxisStyle("Price")
    
    // Save
    let fileName = $"favourite_chart_{System.DateTime.Now:yyyyMMdd_HHmmss}.html"
    chart |> Chart.saveHtml(fileName)
    
    do! report $"Chart saved: {fileName}"
    do! report $"Data points: {timePriceVolumes.Length}"
    do! report $"Price range: {Array.min prices:F2} - {Array.max prices:F2}"

| Some timePriceVolumes ->
    do! report "Price history data is empty"
    
| None -> 
    do! report $"Failed to map 'timePriceVolumes' data for {selection.Name}!"
```

## Chart Characteristics
- Interactive zoom/pan functionality (Plotly default)
- Hover tooltips showing exact time, price, and volume
- Line chart with optional scatter markers
- Marker size proportional to volume (optional)
- Clear title with selection name and market info
- Responsive layout
- Professional styling with grid lines
- Saves as standalone HTML file

