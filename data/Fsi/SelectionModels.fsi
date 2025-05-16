namespace BeloSoft.Bfexplorer.Domain
    
    module DataContext =
        
        /// <summary>
        /// CreatePriceGridDataContext
        /// </summary>
        /// <param name="priceGridData"></param>
        val mutable CreatePriceGridDataContext:
          (PriceGridData -> IPriceGridDataContext)
        
        /// <summary>
        /// UseMarketStreaming
        /// </summary>
        val mutable UseMarketStreaming: bool
    
    /// <summary>
    /// SelectionMetaData
    /// </summary>
    [<NoEquality; NoComparison>]
    type SelectionMetaData =
        {
          Age: byte
          ClothNumber: byte
          StallDraw: byte
          Weight: byte
          Form: string
          OfficialRating: byte
          ForecastPrice: float
          JockeyName: string
          TrainerName: string
          ColoursFileName: string
          BredCountry: string
          DamName: string
          DamBredCountry: string
        }
        
        member HaveColoursImage: bool
        
        member ImageUrl: string
    
    /// <summary>
    /// SelectionStatus
    /// </summary>
    [<Struct>]
    type SelectionStatus =
        | Active = 1
        | Winner = 2
        | Placed = 3
        | Loser = 4
        | RemovedVacant = 5
        | Removed = 6
        | Hidden = 7
    
    /// <summary>
    /// SelectionId
    /// </summary>
    type SelectionId = int64
    
    /// <summary>
    /// SelectionIdentity
    /// </summary>
    type SelectionIdentity =
        {
          Id: SelectionId
          Handicap: float
        }
        
        static member
          Create: id: SelectionId * handicap: float -> SelectionIdentity
        
        static member CreateFromId: id: SelectionId -> SelectionIdentity
        
        static member TryParse: text: string -> SelectionIdentity
        
        override ToString: unit -> string
    
    /// <summary>
    /// DataContextAndObservableObjectUpdated
    /// </summary>
    type DataContextAndObservableObjectUpdated =
        inherit BeloSoft.Data.ObservableObjectUpdated
        
        new: unit -> DataContextAndObservableObjectUpdated
        
        /// <summary>
        /// ContainsData
        /// </summary>
        /// <param name="name"></param>
        member ContainsData: name: string -> bool
        
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="name"></param>
        member GetData: name: string -> 'T option
        
        /// <summary>
        /// GetOrSetData
        /// </summary>
        /// <param name="name"></param>
        member GetOrSetData: name: string -> 'T when 'T: (new: unit -> 'T)
        
        /// <summary>
        /// SetData
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        member SetData: name: string * data: 'T -> unit
        
        /// <summary>
        /// Data
        /// </summary>
        member Data: Map<string,obj>
    
    /// <summary>
    /// Selection
    /// </summary>
    [<System.Diagnostics.DebuggerDisplay ("{Name} - {LastPriceTraded}")>]
    type Selection =
        inherit DataContextAndObservableObjectUpdated
        interface System.IEquatable<Selection>
        
        new: identity: SelectionIdentity * name: string *
             ?metaData: SelectionMetaData -> Selection
        
        /// <summary>
        /// GetAvailableBestPrice
        /// </summary>
        /// <param name="betType"></param>
        member GetAvailableBestPrice: betType: BetType -> PriceSize
        
        /// <summary>
        /// GetProfitBalance
        /// </summary>
        member GetProfitBalance: unit -> float
        
        /// <summary>
        /// SetBetsStatus
        /// </summary>
        member SetBetsStatus: unit -> unit
        
        /// <summary>
        /// SetPriceGridDataProfit
        /// </summary>
        member SetPriceGridDataProfit: unit -> unit
        
        /// <summary>
        /// ToString
        /// </summary>
        override ToString: unit -> string
        
        member AdjustmentFactor: float with get, set
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member BetPosition: BetPosition
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member Bets: SelectionBets
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member BetsCache: BetsCache
        
        member CanCloseBetPosition: bool
        
        member HaveBets: bool
        
        member HaveMatchedBets: bool
        
        member HaveUnmatchedBets: bool
        
        member Identity: SelectionIdentity
        
        [<System.ComponentModel.Browsable (false)>]
        member Index: byte with get, set
        
        member IsInProfit: bool
        
        [<System.ComponentModel.Browsable (false)>]
        member IsWatched: bool with get, set
        
        member LastPriceTraded: float with get, set
        
        member LastPriceTradedBetType: BetType
        
        member MetaData: SelectionMetaData option
        
        member Name: string
        
        [<System.ComponentModel.Browsable (false)>]
        member OddsContext: OddsContext
        
        [<System.ComponentModel.Browsable (false)>]
        member PriceGridData: PriceGridData
        
        [<System.ComponentModel.Browsable (false)>]
        member PriceGridDataEnabled: bool with get, set
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member PriceTradedHistory: PriceTradedHistory
        
        member Profit: System.Nullable<float> with get, set
        
        member ProfitBalance: System.Nullable<float>
        
        member SettledProfit: System.Nullable<float> with get, set
        
        [<System.ComponentModel.Browsable (false)>]
        member ShowInChart: bool with get, set
        
        member Status: SelectionStatus with get, set
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member ToBack: BestOfferedPrices
        
        [<System.ComponentModel.TypeConverter
          (typeof< System.ComponentModel.ExpandableObjectConverter>)>]
        member ToLay: BestOfferedPrices
        
        member TotalMatched: float with get, set
        
        [<System.ComponentModel.Browsable (false)>]
        member TotalMatchedHistory: BeloSoft.Data.HistoryValue<float>
        
        member WhatIfProfit: System.Nullable<float> with get, set
    
    /// <summary>
    /// Bet
    /// </summary>
    and [<Class>] Bet =
        inherit BeloSoft.Data.ObservableObject
        interface System.IEquatable<Bet>
        
        new: id: BetId * selection: Selection * betType: BetType * price: float *
             size: float * time: System.DateTime * orderStatus: BetOrderStatus *
             orderType: OrderType * persistenceType: PersistenceType *
             betStatus: BetStatus -> Bet
        
        /// <summary>
        /// ToString
        /// </summary>
        override ToString: unit -> string
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newPrice"></param>
        /// <param name="newSize"></param>
        /// <param name="newPersistenceType"></param>
        /// <param name="newOrderStatus"></param>
        /// <param name="newBetStatus"></param>
        member
          Update: newPrice: float * newSize: float *
                  newPersistenceType: PersistenceType *
                  newOrderStatus: BetOrderStatus * newBetStatus: BetStatus ->
                    bool
        
        member BetStatus: BetStatus with get, set
        
        member BetType: BetType
        
        member Id: BetId
        
        member OrderStatus: BetOrderStatus with get, set
        
        member OrderType: OrderType
        
        member PersistenceType: PersistenceType with get, set
        
        member Price: float with get, set
        
        member Selection: Selection
        
        member Size: float with get, set
        
        member Time: System.DateTime with get, set
    
    /// <summary>
    /// BetUpdateContextType
    /// </summary>
    and [<Struct>] internal BetUpdateContextType =
        | Added = 1
        | Removed = 2
    
    /// <summary>
    /// BetUpdateContext
    /// </summary>
    and [<Class>] internal BetUpdateContext =
        
        new: index: int * updateType: BetUpdateContextType * bet: Bet ->
               BetUpdateContext
        
        member
          SyncBet: betsToSync: System.Collections.ObjectModel.Collection<Bet> *
                   syncFromIndex: int -> unit
    
    /// <summary>
    /// BetUpdateContextInPracticeMode
    /// </summary>
    and [<Class>] BetUpdateContextInPracticeMode =
        
        new: unit -> BetUpdateContextInPracticeMode
        
        member AddBetIdToCancel: id: BetId -> unit
        
        member AddBetIdsToCancel: ids: BetId seq -> unit
        
        member AddBetToPlace: bet: BetCache -> unit
        
        member AddBetsToPlace: bets: BetCache seq -> unit
        
        member Clear: unit -> unit
        
        member GetIsValidBet: bet: BetCache -> bool
        
        member BetIdsToCancel: ResizeArray<BetId>
        
        member BetsToPlace: ResizeArray<BetCache>
        
        member HaveUpdateContext: bool
    
    /// <summary>
    /// SelectionBets
    /// </summary>
    and [<Class>] SelectionBets =
        inherit System.Collections.ObjectModel.ObservableCollection<Bet>
        
        new: unit -> SelectionBets
        
        /// <summary>
        /// AddBet
        /// </summary>
        /// <param name="bet"></param>
        member AddBet: bet: Bet -> unit
        
        /// <summary>
        /// ClearBets
        /// </summary>
        member ClearBets: unit -> unit
        
        /// <summary>
        /// GetBet
        /// </summary>
        /// <param name="betId"></param>
        member GetBet: betId: BetId -> Bet option
        
        /// <summary>
        /// InsertBetAt
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="index"></param>
        member InsertBetAt: bet: Bet * index: int -> unit
        
        /// <summary>
        /// RemoveBetAt
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="index"></param>
        member RemoveBetAt: bet: Bet * index: int -> unit
        
        /// <summary>
        /// SyncBetUpdateContext
        /// </summary>
        /// <param name="betsToSync"></param>
        /// <param name="syncFromIndex"></param>
        member
          SyncBetUpdateContext: betsToSync: System.Collections.ObjectModel.Collection<Bet> *
                                syncFromIndex: int -> unit
        
        member BetUpdateContextInPracticeMode: BetUpdateContextInPracticeMode
        
        member HaveBets: bool
        
        member IsBetsSyncNeeded: bool
    
    /// <summary>
    /// PriceTradedHistory
    /// </summary>
    and [<Class>] PriceTradedHistory =
        inherit BeloSoft.Data.ObservableObject
        
        new: unit -> PriceTradedHistory
        
        /// <summary>
        /// ResetMinimalMaximalPrice
        /// </summary>
        member ResetMinimalMaximalPrice: unit -> unit
        
        /// <summary>
        /// SetDataAtTime
        /// </summary>
        /// <param name="time"></param>
        /// <param name="selection"></param>
        member
          SetDataAtTime: time: System.DateTime * selection: Selection -> unit
        
        /// <summary>
        /// SetMinimalMaximalPrice
        /// </summary>
        /// <param name="price"></param>
        member SetMinimalMaximalPrice: price: float -> bool
        
        member LastPriceTraded: float
        
        member LastSizeTraded: float
        
        member LastTimeTraded: System.DateTime
        
        member MaximalBackOfferedSize: float with get, set
        
        member MaximalIndicator: float with get, set
        
        member MaximalLayOfferedSize: float with get, set
        
        member MaximalOfferedSize: float with get, set
        
        member MaximalPriceTraded: float
        
        member MaximalTradedVolume: float with get, set
        
        member MinimalPriceTraded: float
        
        member MyValues: TimeHistoryValueCollection<float>
        
        member MyValuesEnabled: bool with get, set
        
        member PriceTrend: int
        
        member Prices: HistoryValueCollection<TimeValue<float>>
        
        [<System.ComponentModel.Browsable (false)>]
        member TradedPrices: HistoryValueCollection<TimePriceTraded>
    
    /// <summary>
    /// SelectionBetOrder
    /// </summary>
    [<NoEquality; NoComparison>]
    type SelectionBetOrder =
        {
          Selection: Selection
          BetType: BetType
          Price: float
          Size: float
          Liability: float option
        }
        
        static member
          Create: selection: Selection * betType: BetType * price: float *
                  size: float * ?liability: float -> SelectionBetOrder
    
    /// <summary>
    /// SelectableSelection
    /// </summary>
    type SelectableSelection =
        inherit BeloSoft.Data.ObservableObject
        
        new: selection: Selection * isSelected: bool -> SelectableSelection
        
        member IsSelected: bool with get, set
        
        member LastPriceTraded: float
        
        member Name: string
        
        member Selection: Selection
        
        member TotalMatched: float
    
    /// <summary>
    /// ISelection
    /// </summary>
    type ISelection =
        
        abstract GetSelection: unit -> Selection
    
    /// <summary>
    /// IBet
    /// </summary>
    type IBet =
        
        abstract BetType: BetType
        
        abstract Price: float
        
        abstract Size: float
    
    /// <summary>
    /// ISelectionProfitLiability
    /// </summary>
    type ISelectionProfitLiability =
        
        abstract Recalculate: unit -> unit
        
        abstract Recalculate: IBet seq -> unit
        
        abstract Identity: SelectionIdentity
        
        abstract Liability: float
        
        abstract Profit: float
    
    /// <summary>
    /// ISelectionPriceGridDataContext
    /// </summary>
    type ISelectionPriceGridDataContext =
        
        abstract Update: Selection -> unit

