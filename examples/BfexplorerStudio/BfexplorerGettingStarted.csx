// Reference assemblies
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Data.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Betfair.API.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.API.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.Host.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.Domain.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.Service.Core.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.Service.dll"
#r "E:/Projects/Bfexplorer/Development/Applications/BeloSoft.Bfexplorer.App/bin/Debug/net10.0-windows/BeloSoft.Bfexplorer.Interoperability.dll"

using System;
using System.Threading.Tasks;
using BeloSoft.Data;
using BeloSoft.Bfexplorer;
using BeloSoft.Bfexplorer.Service;
using BeloSoft.Bfexplorer.Domain;
using BeloSoft.Bfexplorer.Interoperability;

// Initialize bfexplorer service
var bfexplorerService = new BfexplorerService(initializeBotManager: false)
{
	UiApplication = new BfexplorerHost()
};

var credentials = Functions.GetMyBetfairCredentials();

if (credentials != null)
{
	var (username, password) = credentials.Value;

	var loginResult = await bfexplorerService.Login(username, password);

	if (loginResult is DataSuccessFailure.Success)
	{
		var resultAccountFunds = await bfexplorerService.GetAccountFunds();

		if (resultAccountFunds is DataResult<AccountFunds>.Success success)
		{
			Console.WriteLine($"My account balance {success.Value.AvailableToBetBalance:F2}\n");
		}
		else if (resultAccountFunds is DataResult<AccountFunds>.Failure failure)
		{
			Console.WriteLine(failure.ErrorMessage);
		}
	}
	else if (loginResult is DataSuccessFailure.Failure failure)
	{
		Console.WriteLine($"Failed to login: {failure.ErrorMessage}");
	}
}
else
{
	Console.WriteLine("Please set your credentials to the enviroment variables: BETFAIR_USERNAME, BETFAIR_PASSWORD.");
}

string marketId = string.Empty;

async Task GetActiveMarketAsync()
{
	var bfexplorerApplication = Services.CreateBfexplorerApplicationClient();
    
	try
	{
		var marketResult = await bfexplorerApplication.GetActiveMarket();

		return DataResult<MarketInfo>.Success(marketResult);
	}
	catch (Exception ex)
	{
		return DataResult<MarketInfo>.Failure(ex.Message);
	}
}

var activeMarketResult = await GetActiveMarketAsync();

if (activeMarketResult is DataResult<MarketInfo>.Success success)
{
	marketId = success.Value.MarketId;
	//Variables.Set("marketId", marketId);
	Console.WriteLine($"The active market in bfexplorer: {marketId}");
}
else if (activeMarketResult is DataResult<MarketInfo>.Failure failure)
{
	Console.WriteLine($"Error: {failure.ErrorMessage}");
}

void ShowMarket(Market market)
{
	Console.WriteLine($"{market.MarketFullName}\n");

	var selections = Functions.getActiveSelections(market);
	
	foreach (var selection in selections)
	{
		Console.WriteLine($"{selection.Name} ~ {selection.LastPriceTraded:F2} | {selection.TotalMatched:F2}");
	}
}

Console.WriteLine($"Get market data for: {marketId}");

Market market = null;

var marketResult = await bfexplorerService.GetMarket(marketId);

if (marketResult is DataResult<Market>.Success success)
{
	market = success.Value;

	//Variables.Set("myMarket", market);
	ShowMarket(market);
}
else if (marketResult is DataResult<Market>.Failure failure)
{
	Console.WriteLine($"Failed to get market: {failure.ErrorMessage}");
}

var updateResult = await bfexplorerService.UpdateMarket(market);

if (updateResult is Result.Success)
{
	ShowMarket(market);
}
else if (updateResult is Result.Failure failure)
{
	Console.WriteLine($"Failed to update market: {failure.ErrorMessage}");
}

