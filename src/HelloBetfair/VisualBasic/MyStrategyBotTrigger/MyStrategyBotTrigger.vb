Public Class MyStrategyBotTrigger
    Implements IBotTrigger

    Private ReadOnly _market As Market
    Private ReadOnly _selection As Selection
    Private ReadOnly _botName As String
    Private ReadOnly _botTriggerParameters As BotTriggerParameters
    Private ReadOnly _myBfexplorer As IMyBfexplorer

    Public Sub New(market As Market, selection As Selection, botName As String, botTriggerParameters As BotTriggerParameters, myBfexplorer As IMyBfexplorer)
        _market = market
        _selection = selection
        _botName = botName
        _botTriggerParameters = botTriggerParameters
        _myBfexplorer = myBfexplorer
    End Sub

    Private Function GetParameters() As (FromPrice As Double, ToPrice As Double)
        Dim fromPriceOption = _botTriggerParameters.GetParameter(Of Double)("FromPrice")
        Dim toPriceOption = _botTriggerParameters.GetParameter(Of Double)("ToPrice")

        Dim fromPrice = If(OptionModule.IsSome(fromPriceOption), fromPriceOption.Value, 2.5)
        Dim toPrice = If(OptionModule.IsSome(toPriceOption), toPriceOption.Value, 3.0)

        Return (fromPrice, toPrice)
    End Function

    Private Function GetMySelection() As Selection
        Dim parameters = GetParameters()

        Dim fromPrice = parameters.FromPrice
        Dim toPrice = parameters.ToPrice

        Return MarketExtensionsModule.getFavouriteSelections(_market) _
            .FirstOrDefault(Function(s) s.LastPriceTraded >= fromPrice AndAlso s.LastPriceTraded <= toPrice)
    End Function

    Public Function Execute() As TriggerResult Implements IBotTrigger.Execute
        Dim mySelection = GetMySelection()

        If mySelection IsNot Nothing Then
            Return TriggerResult.NewExecuteActionBotOnSelection(mySelection)
        Else
            Return TriggerResult.NewEndExecutionWithMessage("No selection fulfills my criteria.")
        End If
    End Function

    Public Sub EndExecution() Implements IBotTrigger.EndExecution
    End Sub
End Class
