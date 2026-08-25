// Bfexplorer cannot be held responsible for any losses or damages incurred during the use of this betfair bot.
// It is up to you to determine the level of risk you wish to trade under. 
// Do not gamble with money you cannot afford to lose.

module BotTriggerExample

#I @"C:\Program Files\BeloSoft\Bfexplorer\"
//#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"

#r "DevExpress.Data.v25.2.dll"
#r "DevExpress.Office.v25.2.Core.dll"
#r "DevExpress.Printing.v25.2.Core.dll"
#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Trading.dll"

open System
open DevExpress.Spreadsheet

open BeloSoft.Data
open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.Trading

/// <summary>
/// BfexplorerSpreadsheetReadWrite
/// </summary>
type BfexplorerSpreadsheetReadWrite (market : Market, selection : Selection, botName : string, botTriggerParameters : BotTriggerParameters, myBfexplorer : IMyBfexplorer) =
    inherit BotTriggerBase (market, selection, botName, botTriggerParameters, myBfexplorer)

    let bfexplorerSpreadsheet = myBfexplorer.BfexplorerService.Bfexplorer.BfexplorerSpreadsheet

    let mutable worksheet : Worksheet = nil

    let getTextValue (row, column) =
        worksheet.[row, column].DisplayText

    let setValue (row, column) value =
        worksheet.[row, column].SetValue value

    let getEmptyRowIndex () =
        let mutable row = 0

        while not (String.IsNullOrEmpty (getTextValue (row, 0))) do
            row <- row + 1

        row

    let createWorksheet (worksheetName : string) =
        let workbook = bfexplorerSpreadsheet.Document
        
        worksheet <- 
            if workbook.Worksheets.Contains worksheetName
            then
                workbook.Worksheets.[worksheetName]
            else
                workbook.Worksheets.Add worksheetName

    let isBfexplorerSpreadsheetOpen () =
        isNotNullObj bfexplorerSpreadsheet

    let writeMarketData () =
        let row = getEmptyRowIndex ()

        setValue (row, 0) market.MarketFullName
        setValue (row, 1) market.TotalMatched

    interface IBotTrigger with

        /// <summary>
        /// Execute
        /// </summary>
        member this.Execute () =
            if isBfexplorerSpreadsheetOpen ()
            then
                createWorksheet "MyTest"
                writeMarketData ()

                TriggerResult.EndExecution
            else
                TriggerResult.EndExecutionWithMessage "Open bfexplorer spreadsheet application!"

        /// <summary>
        /// EndExecution
        /// </summary>
        member this.EndExecution () =
            ()
