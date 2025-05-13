# Testing strategies

## Requirements

If you want to test any of the published strategies, you must first create a [Betfair account](https://register.betfair.com/account/registration). Then, install the Bfxplorer app from [this link](https://drive.google.com/file/d/1_Ta7K3Spv9WoPV_m5GLzQvJm9x8GqN_J/view?usp=sharing) and enable the URL scheme for opening the Bfxplorer app by [importing it](blob:https://github.com/ede2902b-26b3-454a-baed-e42d09f87c25) into your computer's registry.

![Open Bfxplorer!](/docs/images/OpenBfxplorerURI.png "Open Bfxplorer")

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

* [Bfxplorer latest release](http://Bfxplorer.net/Community/BlogContent/596#Bfxplorer%202025%20Preview)
* [Bfxplorer scheme protocol registry file](/data/BfxplorerSchemeProtocol.reg)