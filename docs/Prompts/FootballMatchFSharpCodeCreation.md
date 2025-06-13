# F# Football Match Code Creation/Update Prompt

When creating or updating football match code in F#, use **ONLY** the following publicly available types, properties, functions, and methods from the provided signature files:

## FootballMatch Type (from FootballMatchModels.fsi)

### Constructor
- `new: market: BeloSoft.Bfexplorer.Domain.Market -> FootballMatch`

### Properties (get/set)
- `AwayNumberOfCorners: byte`
- `AwayNumberOfRedCards: byte` 
- `AwayNumberOfYellowCards: byte`
- `AwayScore: byte`
- `HomeNumberOfCorners: byte`
- `HomeNumberOfRedCards: byte`
- `HomeNumberOfYellowCards: byte`
- `HomeScore: byte`
- `IsInProgess: bool`
- `MatchTime: int`
- `Status: string`

### Read-only Properties
- `AwayTeam: string`
- `CornersDifference: sbyte`
- `Country: string`
- `GoalBeingScored: bool`
- `Goals: byte`
- `HomeTeam: string`
- `Id: int`
- `League: string`
- `Market: BeloSoft.Bfexplorer.Domain.Market`
- `Match: string`
- `Score: string`
- `ScoreDifference: sbyte`

## FootballMatch Operations (from FootballMatchModels.fsi)

### Functions
- `toFootballMatch: market: BeloSoft.Bfexplorer.Domain.Market -> FootballMatch`
- `toFootballMatchResourceLocker: timeToLock: float -> market: BeloSoft.Bfexplorer.Domain.Market -> BeloSoft.Data.ResourceLocker`

## Market Type (from MarketModels.fsi)

### Key Properties
- `Id: MarketId`
- `IsInPlay: bool`
- `MarketStatus: MarketStatus`
- `MarketInfo: MarketInfo`
- `MarketDescription: MarketDescription`
- `Selections: System.Collections.ObjectModel.ObservableCollection<Selection>`
- `TotalMatched: float`
- `UpdateTime: System.DateTime`
- `BetDelay: int`
- `BackBook: float`
- `LayBook: float`
- `MonitoringStatus: MonitoringStatus`
- `MyDescription: obj`
- `ProfitBalance: System.Nullable<float>`
- `SettledProfit: System.Nullable<float>`
- `Version: int64`

### Computed Properties
- `CanCloseBetPosition: bool`
- `HaveMatchedBets: bool`
- `HaveUnmatchedBets: bool`
- `IsInProfit: bool`
- `IsTradingAllowed: bool`
- `MarketFullName: string`
- `MarketStatusText: string`

## MarketInfo Type (from MarketModels.fsi)

### Properties
- `Id: MarketId`
- `MarketName: string`
- `StartTime: System.DateTime`
- `StartTimeUTC: System.DateTime`
- `BetEvent: BetEvent`
- `BetEventType: BetEventType`
- `EventName: string`
- `EventTypeName: string`

## BetEvent Type (from MarketModels.fsi)

### Properties
- `Id: int`
- `Name: string`
- `OpenTimeUTC: System.DateTime`
- `Details: string`
- `CountryCode: string`
- `OpenTime: System.DateTime`

## BetEventType Type (from MarketModels.fsi)

### Properties
- `Id: int`
- `Name: string`

## Selection Type (from SelectionModels.fsi)

### Key Properties
- `Identity: SelectionIdentity`
- `Name: string`
- `Status: SelectionStatus`
- `LastPriceTraded: float`
- `TotalMatched: float`
- `AdjustmentFactor: float`
- `Index: byte`
- `IsWatched: bool`
- `Profit: System.Nullable<float>`
- `SettledProfit: System.Nullable<float>`
- `ShowInChart: bool`
- `WhatIfProfit: System.Nullable<float>`
- `PriceGridDataEnabled: bool`

### Complex Properties
- `BetPosition: BetPosition`
- `Bets: SelectionBets`
- `BetsCache: BetsCache`
- `MetaData: SelectionMetaData option`
- `OddsContext: OddsContext`
- `PriceGridData: PriceGridData`
- `PriceTradedHistory: PriceTradedHistory`
- `ToBack: BestOfferedPrices`
- `ToLay: BestOfferedPrices`
- `TotalMatchedHistory: BeloSoft.Data.HistoryValue<float>`

### Computed Properties
- `CanCloseBetPosition: bool`
- `HaveBets: bool`
- `HaveMatchedBets: bool`
- `HaveUnmatchedBets: bool`
- `IsInProfit: bool`
- `LastPriceTradedBetType: BetType`
- `ProfitBalance: System.Nullable<float>`

### Methods
- `GetAvailableBestPrice: betType: BetType -> PriceSize`
- `GetProfitBalance: unit -> float`

## SelectionIdentity Type (from SelectionModels.fsi)

### Properties
- `Id: SelectionId`
- `Handicap: float`

## Enums and Status Types

### MarketStatus
- `Inactive = 1`
- `Open = 2` 
- `Suspended = 3`
- `Closed = 4`

### MonitoringStatus
- `Active = 1`
- `Passive = 2`
- `Inactive = 3`

### SelectionStatus
- `Active = 1`
- `Winner = 2`
- `Placed = 3`
- `Loser = 4`
- `RemovedVacant = 5`
- `Removed = 6`
- `Hidden = 7`

## Market Extension Functions (from MarketExtensions.fsi)

### Market State Functions
- `isOpenMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool`
- `isClosedMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool`
- `canMonitorMarket: market: BeloSoft.Bfexplorer.Domain.Market -> bool`

### Selection Functions
- `getActiveSelections: market: BeloSoft.Bfexplorer.Domain.Market -> BeloSoft.Bfexplorer.Domain.Selection list`
- `getFavouriteSelections: market: BeloSoft.Bfexplorer.Domain.Market -> BeloSoft.Bfexplorer.Domain.Selection list`
- `getNumberOfActiveSelections: market: BeloSoft.Bfexplorer.Domain.Market -> byte`

## Selection Extension Functions (from SelectionExtensions.fsi)

### Selection State Functions
- `isActiveSelection: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool`
- `isValidSelectionBetPosition: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool`
- `isWinnerSelection: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool`
- `isNotRemovedSelection: selection: BeloSoft.Bfexplorer.Domain.Selection -> bool`

### Selection Lookup Functions
- `tryGetSelectionById: identity: BeloSoft.Bfexplorer.Domain.SelectionIdentity -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection option`
- `getSelectionById: identity: BeloSoft.Bfexplorer.Domain.SelectionIdentity -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection`
- `tryGetSelectionByName: name: string -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection option`
- `tryGetSelection: aSelection: BeloSoft.Bfexplorer.Domain.Selection -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection option`
- `tryGetSelectionByIdAndHandicap: selectionId: int64 * handicap: float -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection option`
- `getSelectionByIdAndHandicap: selectionId: int64 * handicap: float -> selections: BeloSoft.Bfexplorer.Domain.Selection seq -> BeloSoft.Bfexplorer.Domain.Selection`

### Selection Utility Functions
- `getSelectionProfit: selection: BeloSoft.Bfexplorer.Domain.Selection -> float`

### Selection Extension Methods
- `GetBestExecutionPrice: betType: BeloSoft.Bfexplorer.Domain.BetType * ?price: float -> float`
- `GetCanHedgeBetPosition: unit -> bool`
- `GetBetOrderToHedge: unit -> BeloSoft.Bfexplorer.Domain.BetOrder option`
- `GetPriceSize: betType: BeloSoft.Bfexplorer.Domain.BetType * ?offerMyBet: bool -> BeloSoft.Bfexplorer.Domain.PriceSize`
- `GetPriceSize: betType: BeloSoft.Bfexplorer.Domain.BetType * minimumPrice: float * maximumPrice: float * ?offerMyBet: bool -> BeloSoft.Bfexplorer.Domain.PriceSize option`
- `GetPrice: betType: BeloSoft.Bfexplorer.Domain.BetType * ?offerMyBet: bool * ?priceImprovement: sbyte -> float option`
- `GetPrice: betType: BeloSoft.Bfexplorer.Domain.BetType * minimumPrice: float * maximumPrice: float * ?offerMyBet: bool * ?priceImprovement: sbyte -> float option`
- `GetBestPrice: betType: BeloSoft.Bfexplorer.Domain.BetType -> float`
- `GetOfferedPriceDifference: unit -> int`
- `GetWeightOfMoney: unit -> float`
- `GetMetaData: getValue: (BeloSoft.Bfexplorer.Domain.SelectionMetaData -> 'T) -> 'T`

## Important Constraints

1. **DO NOT** create new properties or methods not listed above
2. **ONLY** use the publicly available members from the signature files
3. **DO NOT** access private or internal members
4. Use proper F# syntax and conventions
5. Handle option types appropriately with pattern matching
6. Use the provided constructor signatures exactly as specified
7. Use mutable properties only where explicitly marked with `get, set`
8. Handle nullable types (`System.Nullable<'T>`) appropriately

## Type Aliases

- `MarketId = string`
- `SelectionId = int64`

## Example Usage Patterns

### Working with Markets
```fsharp
// Check market status
if isOpenMarket market then
    // Process active market
    let activeSelections = getActiveSelections market
    let numberOfSelections = getNumberOfActiveSelections market
    
    // Get first selection
    let firstSelection = getFirstMarketSelection market
```

When writing code, ensure you only use the exact property names, method signatures, and types as defined in the signature files above.
