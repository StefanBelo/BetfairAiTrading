namespace BeloSoft.Bfexplorer.FootballScoreProvider.Models
    
    /// <summary>
    /// FootballMatch
    /// </summary>
    type FootballMatch =
        inherit BeloSoft.Data.ObservableObjectUpdated
        
        new: market: BeloSoft.Bfexplorer.Domain.Market -> FootballMatch
        
        override ClearUpdated: unit -> unit
        
        member SetGoalScored: unit -> unit
        
        override ToString: unit -> string
        
        member AwayNumberOfCorners: byte with get, set
        
        member AwayNumberOfRedCards: byte with get, set
        
        member AwayNumberOfYellowCards: byte with get, set
        
        member AwayScore: byte with get, set
        
        member AwayTeam: string
        
        member CornersDifference: sbyte
        
        member Country: string
        
        member GoalBeingScored: bool
        
        member Goals: byte
        
        member HomeNumberOfCorners: byte with get, set
        
        member HomeNumberOfRedCards: byte with get, set
        
        member HomeNumberOfYellowCards: byte with get, set
        
        member HomeScore: byte with get, set
        
        member HomeTeam: string
        
        member Id: int
        
        member IsInProgess: bool with get, set
        
        member League: string
        
        member Market: BeloSoft.Bfexplorer.Domain.Market
        
        member Match: string
        
        member MatchTime: int with get, set
        
        member Score: string
        
        member ScoreDifference: sbyte
        
        member Status: string with get, set
    
    module private DataKeys =
        
        [<Literal>]
        val FootballMatch: string = "FootballMatch"
        
        [<Literal>]
        val FootballMatchResourceLocker: string = "FootballMatchResourceLocker"
    
    [<AutoOpen>]
    module FootballMatchOperations =
        
        /// <summary>
        /// toFootballMatch
        /// </summary>
        /// <param name="market"></param>
        val toFootballMatch:
          market: BeloSoft.Bfexplorer.Domain.Market -> FootballMatch
        
        /// <summary>
        /// toFootballMatchResourceLocker
        /// </summary>
        /// <param name="timeToLock"></param>
        /// <param name="market"></param>
        val toFootballMatchResourceLocker:
          timeToLock: float ->
            market: BeloSoft.Bfexplorer.Domain.Market ->
            BeloSoft.Data.ResourceLocker

