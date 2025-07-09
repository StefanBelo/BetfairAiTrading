# BotTrigger Request Template

**Instructions for the User:** Please fill out the "User Bot Request" section below. Describe the bot's logic in plain language.

**Instructions for the AI Developer (Gemini):** When you receive a request from this file, you MUST generate an F# script that adheres to the technical requirements outlined in the "AI Developer Instructions" section. The user's request defines the *logic*, and your instructions define the required *structure and quality*.

---

## AI Developer Instructions (Internal Rules for Code Generation)

1.  **Structure:**
    *   All code must be within a `BfexplorerBot` module.
    *   Implement a state machine using a `TriggerStatus` discriminated union (e.g., `Initialize`, `Monitor`).
    *   Use a dedicated `record` type to hold state for each selection being watched.
2.  **API Usage:**
    *   Use `botTriggerParameters.GetParameter<'T>` to create configurable settings from the user's request.
    *   Use the most specific `TriggerResult` case possible (e.g., `ExecuteActionBotOnSelectionsAndContinueToExecute` for multiple selections).
    *   Use `this.Report` for any logging requested by the user.
3.  **Best Practices:**
    *   Include market validation checks (e.g., ensure the bot runs on the correct sport or market type if specified).
    *   Ensure the bot terminates correctly (`TriggerResult.EndExecution`) when its work is done or if initialization fails.
    *   The generated code must be robust and production-ready, following the patterns in other script files ending with `BotTrigger.fsx`.
4.  **References:**
    *   Use types, interfaces, and methods defined in the signature files located in `E:\Projects\BetfairAiTrading\data\Fsi\*.fsi`.
    *   Refer to example implementations in files ending with `.fsx` in the `E:\Projects\BetfairAiTrading\src\Strategies` folder that include `*BotTrigger*` in their names.
    *   Ensure compatibility with the `BeloSoft.Bfexplorer` libraries.

---

## User Bot Request (Fill out this section)

**(This is an example based on the "Close By Position Difference" bot. Replace the values with your new bot's requirements.)**

*   **Bot Name:**
    *   `CloseByPositionDifferenceBotTrigger`

*   **Bot Objective (Describe the main goal in one sentence):**
    *   To automatically close a bet if a horse's favoritism rank drops too far.

*   **Market to Run On (Optional, but recommended):**
    *   **Sport:** Horse Racing
    *   **Market Type:** WIN

*   **Trigger Conditions (When should the bot take action?):**
    *   A horse's rank drops by a certain number of positions.
    *   **OR...**
    *   The favorite horse's odds drop below a certain value.

*   **Action to Take (What should the bot do when triggered?):**
    *   Close the bet position for the horse(s) that met the trigger conditions.

*   **Configurable Settings (These will appear as options for the bot):**
    *   **Setting 1:**
        *   **Name:** `PositionDifference`
        *   **Description:** How many positions a horse must drop to trigger the action.
        *   **Default Value:** `2`
    *   **Setting 2:**
        *   **Name:** `MinimalFavouriteOdds`
        *   **Description:** If the favorite's odds are below this, close all monitored horses.
        *   **Default Value:** `0.0`
    *   **Setting 3:**
        *   **Name:** `ShowPositionChanges`
        *   **Description:** Log a message every time a horse's rank changes.
        *   **Default Value:** `false`


