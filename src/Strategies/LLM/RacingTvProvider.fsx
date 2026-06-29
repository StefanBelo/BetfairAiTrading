module RacingTvProvider.Test

#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows"
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\runtimes\win\native"

#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.RacingTvProvider.dll"

open BeloSoft.Bfexplorer.Domain
open BeloSoft.Bfexplorer.RacingTvProvider
    
let doGetRaceCard (market : Market) = async {
    use racingTvProvider = new RacingTvProvider ()

    return! racingTvProvider.GetRaceCard market.MarketInfo
}