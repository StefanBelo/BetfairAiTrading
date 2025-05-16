namespace BeloSoft.Bfexplorer.Domain
    
    /// <summary>
    /// BetEventType
    /// </summary>
    [<NoEquality; NoComparison>]
    type BetEventType =
        {
          Id: int
          Name: string
        }
    
    /// <summary>
    /// BetEvent
    /// </summary>
    [<NoEquality; NoComparison>]
    type BetEvent =
        {
          Id: int
          Name: string
          OpenTimeUTC: System.DateTime
          Details: string
          CountryCode: string
        }
        
        member OpenTime: System.DateTime
    
    /// <summary>
    /// NavigationContext
    /// </summary>
    [<NoEquality; NoComparison>]
    type NavigationContext =
        {
          MarketTypes: string array
          Countries: string array
          Venues: string array
        }
    
    /// <summary>
    /// MarketId
    /// </summary>
    type MarketId = string
    
    /// <summary>
    /// MarketInfo
    /// </summary>
    type MarketInfo =
        
        new: id: MarketId * marketName: string *
             startTimeEntryUTC: System.DateTime * betEventType: BetEventType *
             betEvent: BetEvent -> MarketInfo
        
        override ToString: unit -> string
        
        member BetEvent: BetEvent
        
        member BetEventType: BetEventType
        
        member EventName: string
        
        member EventTypeName: string
        
        member Id: MarketId
        
        member MarketName: string
        
        member StartTime: System.DateTime with get, set
        
        member StartTimeUTC: System.DateTime
    
    /// <summary>
    /// MarketCatalogue
    /// </summary>
    type MarketCatalogue =
        
        new: marketInfo: MarketInfo * totalMatched: float -> MarketCatalogue
        
        member MarketInfo: MarketInfo
        
        member TotalMatched: float
    
    /// <summary>
    /// MarketBettingType
    /// </summary>
    [<Struct>]
    type MarketBettingType =
        | Odds = 1
        | Line = 2
        | AsianHandicapDoubleLine = 3
        | AsianHandicapSingleLine = 4
        | FixedOdds = 5
    
    /// <summary>
    /// MarketDescription
    /// </summary>
    [<NoEquality; NoComparison>]
    type MarketDescription =
        {
          MarketBettingType: MarketBettingType
          MarketType: string
          Details: string
          PersistenceEnabled: bool
          BspMarket: bool
          TurnInPlayEnabled: bool
          CommissionBaseRate: float
          DiscountAllowed: bool
          mutable IsMoreWinnersMarketBettingType: bool
          mutable NumberOfWinners: byte
        }
        
        member IsHandicapMarketBettingType: bool
        
        member IsLineBettingType: bool
    
    /// <summary>
    /// MarketStatus
    /// </summary>
    [<Struct>]
    type MarketStatus =
        | Inactive = 1
        | Open = 2
        | Suspended = 3
        | Closed = 4
    
    /// <summary>
    /// MonitoringStatus
    /// </summary>
    [<Struct>]
    type MonitoringStatus =
        | Active = 1
        | Passive = 2
        | Inactive = 3
    
    /// <summary>
    /// Market
    /// </summary>
    type Market =
        inherit DataContextAndObservableObjectUpdated
        interface System.IEquatable<Market>
        
        new: marketInfo: MarketInfo * marketDescription: MarketDescription *
             selectionsSeq: Selection seq * totalMatched: float -> Market
        
        /// <summary>
        /// ClearUpdated
        /// </summary>
        override ClearUpdated: unit -> unit
        
        /// <summary>
        /// ClearWhatIfProfit
        /// </summary>
        member ClearWhatIfProfit: unit -> unit
        
        /// <summary>
        /// SetMarketStatus
        /// </summary>
        /// <param name="marketStatusValue"></param>
        /// <param name="isInPlayValue"></param>
        member
          SetMarketStatus: marketStatusValue: MarketStatus * isInPlayValue: bool ->
                             bool
        
        /// <summary>
        /// SetTotalMatched
        /// </summary>
        /// <param name="value"></param>
        member SetTotalMatched: value: float -> bool
        
        /// <summary>
        /// ToString
        /// </summary>
        override ToString: unit -> string
        
        /// <summary>
        /// UpdateBetsAndSelectionProfits
        /// </summary>
        member UpdateBetsAndSelectionProfits: unit -> unit
        
        /// <summary>
        /// UpdateProfitBalance
        /// </summary>
        member UpdateProfitBalance: unit -> unit
        
        /// <summary>
        /// UpdateWhatIfProfit
        /// </summary>
        /// <param name="selectionProfitLiabilities"></param>
        member
          UpdateWhatIfProfit: selectionProfitLiabilities: ISelectionProfitLiability seq ->
                                unit
        
        member BackBook: float with get, set
        
        member BetDelay: int with get, set
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member Bets: MarketBets
        
        member CanCloseBetPosition: bool
        
        member HaveMatchedBets: bool
        
        member HaveUnmatchedBets: bool
        
        member Id: MarketId
        
        member IsInPlay: bool
        
        member IsInProfit: bool
        
        member IsTradingAllowed: bool
        
        member LayBook: float with get, set
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member MarketDescription: MarketDescription
        
        member MarketFullName: string
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member MarketInfo: MarketInfo
        
        member MarketStatus: MarketStatus
        
        [<System.ComponentModel.Browsable (false)>]
        member MarketStatusHistory: BeloSoft.Data.HistoryValue<MarketStatus>
        
        member MarketStatusText: string
        
        member MonitoringStatus: MonitoringStatus with get, set
        
        member MyDescription: obj with get, set
        
        member ProfitBalance: System.Nullable<float>
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member ProfitHistory: TimeHistoryValueCollection<float>
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member
          RunningBots: System.Collections.ObjectModel.ObservableCollection<Bot>
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member
          Selections: System.Collections.ObjectModel.ObservableCollection<Selection>
        
        member SettledProfit: System.Nullable<float> with get, set
        
        member TotalMatched: float
        
        member UpdateTime: System.DateTime with get, set
        
        member Version: int64 with get, set
    
    /// <summary>
    /// Bot
    /// </summary>
    and [<AbstractClass; Class>] Bot =
        inherit BeloSoft.Data.ObservableObject
        
        new: name: string -> Bot
        
        abstract CanOperate: unit -> bool
        
        override EndExecution: unit -> unit
        
        abstract EndExecution: unit -> unit
        
        abstract Execute: unit -> unit
        
        override ToString: unit -> string
        
        member BetStatus: BotBetStatus with get, set
        
        member BetStatusDescription: string with get, set
        
        member Name: string
        
        abstract RunningOnSelection: Selection
        
        override RunningOnSelection: Selection
        
        member Status: BotStatus with get, set
    
    /// <summary>
    /// MarketBets
    /// </summary>
    and [<Class>] MarketBets =
        inherit System.Collections.ObjectModel.ObservableCollection<Bet>
        
        new: unit -> MarketBets
        
        member
          SyncSelectionBets: selections: System.Collections.Generic.IList<Selection> ->
                               bool
        
        member BetOperationConfirmationTime: System.DateTime
        
        member BetOperationInProgress: bool with get, set
        
        member HaveBets: bool
        
        member HaveMatchedBets: bool
        
        member HaveUnmatchedBets: bool
        
        member ShouldConfirmBetOperation: bool
    
    /// <summary>
    /// IMarket
    /// </summary>
    type IMarket =
        
        abstract GetMarket: unit -> Market
    
    /// <summary>
    /// MarketSelection
    /// </summary>
    [<NoEquality; NoComparison>]
    type MarketSelection =
        {
          Market: Market
          Selection: Selection
        }
        
        static member
          Create: market: Market * selection: Selection -> MarketSelection
    
    /// <summary>
    /// MyBetResult
    /// </summary>
    [<NoEquality; NoComparison>]
    type MyBetResult =
        {
          BetEventTypeId: int
          BetEventTypeName: string
          BetEventName: string
          MarketId: string
          MarketName: string
          SelectionName: string
          Time: System.DateTime
          LastMatchedTime: System.DateTime
          BetType: System.Nullable<BetType>
          Price: System.Nullable<float>
          Size: System.Nullable<float>
          Profit: float
          StrategyReference: string
        }

