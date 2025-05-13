# Testing strategies

## Requirements

If you want to test any of the published strategies, you must first create a Betfair account. Then, install the Bfxplorer app from this URL and enable the URL scheme for opening the Bfxplorer app by importing it into your computer's registry.

![Open Bfxplorer?!](/docs/images/OpenBfxplorerURI.png "Open Bfxplorer?")

## How does it work?

When the URI is opened, the script file is executed by the Bfxplorer app. This script provides the necessary instructions to configure all required data within the app to successfully run a test strategy.
Here is an example of such a script file:

```json
{
    "BetEvents": [
        "Horse Racing - GB and IE.betevents"
    ],
    "Scripts": [
        "/Strategies/Horse Racing/HorseRacingRaceDistanceBotTrigger.fsx"
    ],
    "Strategies": [
        "Horse Racing - Race Distance.bots"
    ],
    "Execute": {
        "BetEvent": "Horse Racing - GB and IE",
        "Strategy": "Horse Racing - Race Distance",
        "TimeSpanInSeconds": -30
    }
}
```

And here is the result of executing such a script in the Bfxplorer app?

```
Starting to execute the script file:
 Switched the practice mode on.
 Imported the strategy: Horse Racing - Race Distance
 Imported the script file: HorseRacingRaceDistanceBotTrigger.fsx
 Imported the bet event: Horse Racing - GB and IE
 Opened the bet event: Horse Racing - GB and IE
 Trying to open a valid market for the bet event Horse Racing - GB and IE
 Trying to execute the strategy Horse Racing - Race Distance
The end of the script file execution.
```

## Links

* [Bfxplorer lattest release](http://Bfxplorer.net/Community/BlogContent/596#Bfxplorer%202025%20Preview)
* [Bfxplorer scheme protocol registry file](/data/BfxplorerSchemeProtocol.reg)