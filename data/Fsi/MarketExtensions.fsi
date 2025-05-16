
[<AutoOpen; CompilationRepresentation (enum<CompilationRepresentationFlags> (4))>]
module BeloSoft.Bfexplorer.Domain.MarketExtensions

/// <summary>
/// isOpenMarket
/// </summary>
/// <param name="market"></param>
val isOpenMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool

/// <summary>
/// isClosedMarket
/// </summary>
/// <param name="market"></param>
val isClosedMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool

/// <summary>
/// canMonitorMarket
/// </summary>
/// <param name="market"></param>
val canMonitorMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool

/// <summary>
/// setMarketOpen
/// </summary>
/// <param name="market"></param>
val setMarketOpen: market: BeloSoft.Bfexplorer.Domain.Market -> unit

/// <summary>
/// setMarketInactive
/// </summary>
/// <param name="market"></param>
val setMarketInactive: market: BeloSoft.Bfexplorer.Domain.Market -> unit

/// <summary>
/// setSettledProfit
/// </summary>
/// <param name="market"></param>
val setSettledProfit: market: BeloSoft.Bfexplorer.Domain.Market -> unit

/// <summary>
/// getMarketIds
/// </summary>
/// <param name="markets"></param>
val getMarketIds:
  markets: BeloSoft.Bfexplorer.Domain.Market seq ->
    BeloSoft.Bfexplorer.Domain.MarketId array

/// <summary>
/// getMarket
/// </summary>
/// <param name="marketId"></param>
/// <param name="markets"></param>
val getMarket:
  marketId: BeloSoft.Bfexplorer.Domain.MarketId ->
    markets: BeloSoft.Bfexplorer.Domain.Market seq ->
    BeloSoft.Bfexplorer.Domain.Market option

/// <summary>
/// marketExists
/// </summary>
/// <param name="market"></param>
/// <param name="markets"></param>
val marketExists:
  market: BeloSoft.Bfexplorer.Domain.Market ->
    markets: BeloSoft.Bfexplorer.Domain.Market seq -> bool

/// <summary>
/// addBot
/// </summary>
/// <param name="bot"></param>
/// <param name="market"></param>
val addBot:
  bot: BeloSoft.Bfexplorer.Domain.Bot ->
    market: BeloSoft.Bfexplorer.Domain.Market -> unit

/// <summary>
/// getSelectionBots
/// </summary>
/// <param name="selection"></param>
/// <param name="market"></param>
val getSelectionBots:
  selection: BeloSoft.Bfexplorer.Domain.Selection ->
    market: BeloSoft.Bfexplorer.Domain.Market ->
    BeloSoft.Bfexplorer.Domain.Bot list

/// <summary>
/// getActiveSelections
/// </summary>
/// <param name="market"></param>
val getActiveSelections:
  market: BeloSoft.Bfexplorer.Domain.Market ->
    BeloSoft.Bfexplorer.Domain.Selection list

/// <summary>
/// getFavouriteSelections
/// </summary>
/// <param name="market"></param>
val getFavouriteSelections:
  market: BeloSoft.Bfexplorer.Domain.Market ->
    BeloSoft.Bfexplorer.Domain.Selection list

/// <summary>
/// getNumberOfActiveSelections
/// </summary>
/// <param name="market"></param>
val getNumberOfActiveSelections:
  market: BeloSoft.Bfexplorer.Domain.Market -> byte

/// <summary>
/// getFirstMarketSelection
/// </summary>
/// <param name="market"></param>
val getFirstMarketSelection:
  market: BeloSoft.Bfexplorer.Domain.Market ->
    BeloSoft.Bfexplorer.Domain.Selection

