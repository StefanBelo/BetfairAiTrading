
[<AutoOpen; CompilationRepresentation (enum<CompilationRepresentationFlags> (4))>]
module BeloSoft.Bfexplorer.Domain.SelectionExtensions

/// <summary>
/// InitializeSelection
/// </summary>
val mutable InitializeSelection: (BeloSoft.Bfexplorer.Domain.Selection -> unit)

module DummyData =
    
    val NoSelection: BeloSoft.Bfexplorer.Domain.Selection

/// <summary>
/// isActiveSelection
/// </summary>
/// <param name="selection"></param>
val isActiveSelection: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool

/// <summary>
/// isValidSelectionBetPosition
/// </summary>
/// <param name="selection"></param>
val isValidSelectionBetPosition:
  selection: BeloSoft.Bfexplorer.Domain.Selection -> bool

/// <summary>
/// isWinnerSelection
/// </summary>
/// <param name="selection"></param>
val isWinnerSelection: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool

/// <summary>
/// isNotRemovedSelection
/// </summary>
/// <param name="selection"></param>
val isNotRemovedSelection:
  selection: BeloSoft.Bfexplorer.Domain.Selection -> bool

/// <summary>
/// getSelectionProfit
/// </summary>
/// <param name="selection"></param>
val getSelectionProfit: selection: BeloSoft.Bfexplorer.Domain.Selection -> float

/// <summary>
/// tryGetSelectionById
/// </summary>
/// <param name="identity"></param>
/// <param name="selections"></param>
val tryGetSelectionById:
  identity: BeloSoft.Bfexplorer.Domain.SelectionIdentity ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection option

/// <summary>
/// getSelectionById
/// </summary>
/// <param name="identity"></param>
/// <param name="selections"></param>
val getSelectionById:
  identity: BeloSoft.Bfexplorer.Domain.SelectionIdentity ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection

/// <summary>
/// tryGetSelectionByName
/// </summary>
/// <param name="name"></param>
/// <param name="selections"></param>
val tryGetSelectionByName:
  name: string ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection option

/// <summary>
/// tryGetSelection
/// </summary>
/// <param name="aSelection"></param>
/// <param name="selections"></param>
val tryGetSelection:
  aSelection: BeloSoft.Bfexplorer.Domain.Selection ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection option

/// <summary>
/// tryGetSelectionByIdAndHandicap
/// </summary>
/// <param name="selectionId"></param>
/// <param name="handicap"></param>
/// <param name="selections"></param>
val tryGetSelectionByIdAndHandicap:
  selectionId: int64 * handicap: float ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection option

/// <summary>
/// getSelectionByIdAndHandicap
/// </summary>
/// <param name="selectionId"></param>
/// <param name="handicap"></param>
/// <param name="selections"></param>
val getSelectionByIdAndHandicap:
  selectionId: int64 * handicap: float ->
    selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection

/// <summary>
/// sortMySelections
/// </summary>
/// <param name="selections"></param>
val sortMySelections:
  selections: BeloSoft.Bfexplorer.Domain.Selection seq ->
    BeloSoft.Bfexplorer.Domain.Selection seq
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetBestExecutionPrice
    /// </summary>
    /// <param name="betType"></param>
    /// <param name="price"></param>
    member
      GetBestExecutionPrice: betType: BeloSoft.Bfexplorer.Domain.BetType *
                             ?price: float -> float
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetCanHedgeBetPosition
    /// </summary>
    member GetCanHedgeBetPosition: unit -> bool
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetBetOrderToHedge
    /// </summary>
    member
      GetBetOrderToHedge: unit -> BeloSoft.Bfexplorer.Domain.BetOrder option
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetPriceSize
    /// </summary>
    /// <param name="betType"></param>
    /// <param name="offerMyBet"></param>
    member
      GetPriceSize: betType: BeloSoft.Bfexplorer.Domain.BetType *
                    ?offerMyBet: bool -> BeloSoft.Bfexplorer.Domain.PriceSize
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetPriceSize
    /// </summary>
    /// <param name="betType"></param>
    /// <param name="minimumPrice"></param>
    /// <param name="maximumPrice"></param>
    /// <param name="offerMyBet"></param>
    member
      GetPriceSize: betType: BeloSoft.Bfexplorer.Domain.BetType *
                    minimumPrice: float * maximumPrice: float *
                    ?offerMyBet: bool ->
                      BeloSoft.Bfexplorer.Domain.PriceSize option
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetPrice
    /// </summary>
    /// <param name="betType"></param>
    /// <param name="offerMyBet"></param>
    /// <param name="priceImprovement"></param>
    member
      GetPrice: betType: BeloSoft.Bfexplorer.Domain.BetType * ?offerMyBet: bool *
                ?priceImprovement: sbyte -> float option
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetPrice
    /// </summary>
    /// <param name="betType"></param>
    /// <param name="minimumPrice"></param>
    /// <param name="maximumPrice"></param>
    /// <param name="offerMyBet"></param>
    /// <param name="priceImprovement"></param>
    member
      GetPrice: betType: BeloSoft.Bfexplorer.Domain.BetType *
                minimumPrice: float * maximumPrice: float * ?offerMyBet: bool *
                ?priceImprovement: sbyte -> float option
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetBestOdds
    /// </summary>
    /// <param name="betType"></param>
    member GetBestPrice: betType: BeloSoft.Bfexplorer.Domain.BetType -> float
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetOfferedPriceDifference
    /// </summary>
    member GetOfferedPriceDifference: unit -> int
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetWeightOfMoney
    /// </summary>
    member GetWeightOfMoney: unit -> float
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// SetMarker
    /// </summary>
    /// <param name="priceIndex"></param>
    /// <param name="marker"></param>
    member
      SetMarker: priceIndex: int *
                 marker: BeloSoft.Bfexplorer.Domain.TradeStatus -> unit
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// SetMarker
    /// </summary>
    /// <param name="price"></param>
    /// <param name="marker"></param>
    member
      SetMarker: price: float * marker: BeloSoft.Bfexplorer.Domain.TradeStatus ->
                   int
type BeloSoft.Bfexplorer.Domain.Selection with
    
    /// <summary>
    /// GetMetaData
    /// </summary>
    /// <param name="getValue"></param>
    member
      GetMetaData: getValue: (BeloSoft.Bfexplorer.Domain.SelectionMetaData -> 'T) ->
                     'T

