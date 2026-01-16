# F# DSL for Betfair BotTrigger Strategies

I've been experimenting with F# computation expressions to build a domain-specific language (DSL) for Betfair trading strategies. The result is a much more readable and composable way to express strategy logic. For example, my core strategy now looks like this:

```fsharp
let strategy =
    trigger {
        let! fromPrice = param "FromPrice" 2.5
        let! toPrice = param "ToPrice" 3.0
        let! mySelection = favouriteSelectionInRange fromPrice toPrice

        return mySelection
    }
```

This approach lets you focus on *what* you want to do, not the plumbing. It’s clean, testable, and easy to extend. If you’re building trading bots in F#, I highly recommend trying a DSL approach!