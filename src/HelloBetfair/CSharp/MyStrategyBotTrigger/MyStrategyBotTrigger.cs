using BeloSoft.Bfexplorer.Domain;
using BeloSoft.Bfexplorer.Trading;
using Microsoft.FSharp.Core;

namespace MyStrategyBotTrigger
{
    public class MyStrategyBotTrigger(Market market, Selection selection, string botName, BotTriggerParameters botTriggerParameters, IMyBfexplorer myBfexplorer) 
        : IBotTrigger
    {
        private (double FromPrice, double ToPrice) GetParameters()
        {
            var fromPriceOption = botTriggerParameters.GetParameter<double>("FromPrice");
            var toPriceOption = botTriggerParameters.GetParameter<double>("ToPrice");

            var fromPrice = FSharpOption<double>.get_IsSome(fromPriceOption) ? fromPriceOption.Value : 2.5;
            var toPrice = FSharpOption<double>.get_IsSome(toPriceOption) ? toPriceOption.Value : 3.0;

            return (fromPrice, toPrice);
        }

        private Selection GetMySelection()
        {
            var (fromPrice, toPrice) = GetParameters();

            return MarketExtensionsModule.getFavouriteSelections(market)
                .FirstOrDefault(s => s.LastPriceTraded >= fromPrice && s.LastPriceTraded <= toPrice);
        }

        public TriggerResult Execute()
        {
            var mySelection = GetMySelection();
            
            if (mySelection != null)
            {
                return TriggerResult.NewExecuteActionBotOnSelection(mySelection);
            }
            else
            {
                return TriggerResult.NewEndExecutionWithMessage("No selection fulfills my criteria.");
            }
        }

        public void EndExecution()
        {
        }
    }
}
