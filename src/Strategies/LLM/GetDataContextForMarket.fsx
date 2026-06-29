module MyTest

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"

#r "DevExpress.Spreadsheet.v25.2.Core.dll"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Betfair.API.dll"
#r "BeloSoft.Bfexplorer.API.dll"
#r "BeloSoft.Bfexplorer.Host.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.Service.dll"

open System.Collections.Generic

open BeloSoft.Data
open BeloSoft.Bfexplorer.Service
open BeloSoft.Bfexplorer.Domain

let getData<'T> (dataField : string) (data : Dictionary<string, obj>) = 
    try
        let status, myData = data.TryGetValue dataField

        if status
        then        
            Some (myData :?> 'T)
        else
            None
    with
    | _ -> None
    
let doGetMarketDataContext (market : Market) (bfexplorerService : BfexplorerService) = async {
    let bfexplorer = (bfexplorerService :> IBfexplorerService).Bfexplorer
    
    match! bfexplorer.GetDataContextForMarket ([| "RacingTvDataForHorses" |], market) with
    | DataResult.Success marketDataContext ->

        return DataResult.Success marketDataContext

    | DataResult.Failure errorMessage -> return DataResult.Failure errorMessage
}