namespace BeloSoft.Bfexplorer.Trading
    
    /// <summary>
    /// BotTriggerParameters
    /// </summary>
    type BotTriggerParameters =
        
        new: parameters: string * selectionCriteria: string ->
               BotTriggerParameters
        
        /// <summary>
        /// GetEnumParameter
        /// </summary>
        /// <param name="name"></param>
        member GetEnumParameter: name: string -> 'T option
        
        /// <summary>
        /// GetParameter
        /// </summary>
        /// <param name="name"></param>
        member GetParameter: name: string -> 'T option
        
        member BetType: BeloSoft.Bfexplorer.Domain.BetType with get, set
        
        member Odds: float with get, set
        
        member Stake: float with get, set
        
        member StakeType: BeloSoft.Bfexplorer.Domain.StakeType with get, set
        
        static member SelectionCriteriaDataKey: string
    
    /// <summary>
    /// MyBotParameter
    /// </summary>
    [<NoEquality; NoComparison>]
    type MyBotParameter =
        {
          Name: string
          Value: obj
        }
    
    /// <summary>
    /// TriggerResult
    /// </summary>
    type TriggerResult =
        | WaitingForOperation
        | OpenAssociatedMarkets of
          marketNames: string array *
          dataUpdated:
            (BeloSoft.Data.DataResult<BeloSoft.Bfexplorer.Domain.Market list> ->
               unit)
        | WatchMarketSelections of
          market: BeloSoft.Bfexplorer.Domain.Market *
          selections: BeloSoft.Bfexplorer.Domain.Selection list
        | Alert of message: string
        | AlertMessage of message: string
        | UpdateFootballMatchScore of
          footballMatch:
            BeloSoft.Bfexplorer.FootballScoreProvider.Models.FootballMatch *
          dataUpdated: (bool -> unit)
        | UpdateTennisMatchScore of
          tennisMatch:
            BeloSoft.Bfexplorer.TennisScoreProvider.Models.TennisMatch *
          dataUpdated: (bool -> unit)
        | PlaceBets of
          betOrders: BeloSoft.Bfexplorer.Domain.SelectionBetOrder list *
          persistenceType: BeloSoft.Bfexplorer.Domain.PersistenceType *
          betsPlaced: (bool -> unit)
        | CancelBets of
          bets: BeloSoft.Bfexplorer.Domain.Bet list option *
          betsCancelled: (bool -> unit)
        | ExecuteActionBot
        | ExecuteActionBotOnSelection of
          selection: BeloSoft.Bfexplorer.Domain.Selection
        | ExecuteActionBotOnSelectionsAndContinueToExecute of
          selections: BeloSoft.Bfexplorer.Domain.Selection list *
          continueToExecute: bool
        | ExecuteActionBotWithParameters of myBotParameters: MyBotParameter list
        | ExecuteActionBotOnSelectionWithParameters of
          selection: BeloSoft.Bfexplorer.Domain.Selection *
          myBotParameters: MyBotParameter list
        | ExecuteActionBotOnSelectionWithParametersAndContinueToExecute of
          selection: BeloSoft.Bfexplorer.Domain.Selection *
          myBotParameters: MyBotParameter list * continueToExecute: bool
        | ExecuteBfexplorerBotOnSelectionWithParametersAndContinueToExecute of
          botType: System.Type * selection: BeloSoft.Bfexplorer.Domain.Selection *
          botParameters: BeloSoft.Bfexplorer.Domain.BotParameters *
          continueToExecute: bool
        | ExecuteMyActionBotOnSelectionWithParametersAndContinueToExecute of
          botName: string * selection: BeloSoft.Bfexplorer.Domain.Selection *
          myBotParameters: MyBotParameter list * continueToExecute: bool
        | ExecuteActionBotOnMarketSelectionAndContinueToExecute of
          market: BeloSoft.Bfexplorer.Domain.Market *
          selection: BeloSoft.Bfexplorer.Domain.Selection *
          continueToExecute: bool
        | ExecuteMyActionBotOnMarketSelectionAndContinueToExecute of
          botName: string * market: BeloSoft.Bfexplorer.Domain.Market *
          selection: BeloSoft.Bfexplorer.Domain.Selection *
          myBotParameters: MyBotParameter list * continueToExecute: bool
        | EndExecution
        | EndExecutionWithMessage of message: string
    
    /// <summary>
    /// IMyBfexplorer
    /// </summary>
    type IMyBfexplorer =
        
        /// <summary>
        /// BfexplorerService
        /// </summary>
        abstract
          BfexplorerService: BeloSoft.Bfexplorer.Service.IBfexplorerService
        
        /// <summary>
        /// OpenBetEvent
        /// </summary>
        abstract OpenBetEvent: BeloSoft.Bfexplorer.Domain.OpenBetEvent option
    
    /// <summary>
    /// BotTrigger
    /// </summary>
    type IBotTrigger =
        
        /// <summary>
        /// EndExecution
        /// </summary>
        abstract EndExecution: unit -> unit
        
        /// <summary>
        /// Execute
        /// </summary>
        abstract Execute: unit -> TriggerResult
    
    /// <summary>
    /// BotTriggerBase
    /// </summary>
    type BotTriggerBase =
        
        new: market: BeloSoft.Bfexplorer.Domain.Market *
             _selection: BeloSoft.Bfexplorer.Domain.Selection * _botName: string *
             botTriggerParameters: BotTriggerParameters *
             myBfexplorer: IMyBfexplorer -> BotTriggerBase
        
        /// <summary>
        /// GetMySelectionsBySelectionCriteria
        /// </summary>
        /// <param name="mySelectionsData"></param>
        member
          GetMySelectionsBySelectionCriteria: mySelectionsData: 'a list ->
                                                'a list
                                                when 'a :>
                                                          BeloSoft.Bfexplorer.Domain.ISelection
        
        /// <summary>
        /// Report
        /// </summary>
        /// <param name="message"></param>
        member Report: message: string -> unit

