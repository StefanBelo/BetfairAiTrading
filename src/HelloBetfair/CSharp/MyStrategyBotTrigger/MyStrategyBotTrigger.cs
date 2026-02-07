using BeloSoft.Bfexplorer.Domain;
using BeloSoft.Bfexplorer.Trading;
using Microsoft.FSharp.Core;

#pragma warning disable CS9113 // Parameter is unread.
#pragma warning disable CS8603 // Possible null reference return.

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

#pragma warning restore CS9113 // Parameter is unread.
#pragma warning restore CS8603 // Possible null reference return.