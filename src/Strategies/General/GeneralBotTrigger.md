# F# BotTrigger Script Requirements

This document outlines the requirements and best practices for creating F# BotTrigger scripts, which define automated trading logic within the Bfexplorer application.

## 1. File Naming Convention

BotTrigger scripts must follow the naming convention `[StrategyName]BotTrigger.fsx`. The `.fsx` extension indicates an F# script file.

## 2. API Access and Usage

The `data/Fsi` directory contains `.fsi` (F# signature) files. These files are the **definitive source** for understanding the public API available for your BotTrigger scripts. They explicitly define all accessible types, modules, functions, properties, and methods.

**Strict Usage Policy:**
*   You are strictly limited to using only the types, modules, functions, properties, and methods that are explicitly declared within the `.fsi` files provided in the `data/Fsi` directory.
*   Do not assume the existence of any elements not found in these signature files; doing so will lead to compilation errors and incorrect behavior.

**Dependency Management:**
*   **Assembly References (`#r`):** Reference necessary compiled DLLs using `#r`.
*   **`.fsi` Files and `#load`:** While `.fsi` files are essential for understanding the API, you **do not** need to use `#load` directives for them in your `.fsx` scripts if the corresponding compiled DLLs are already referenced via `#r`. The types defined in the `.fsi` files are exposed through these compiled assemblies.
*   **Namespace Imports (`open`):** Ensure all `open` directives correspond to namespaces explicitly defined within the provided `.fsi` files or exposed by the referenced DLLs. If an `open` directive refers to a namespace not found in the referenced assemblies, it will result in a compilation error.

**Key `.fsi` Files to Review:**
*   `MarketModels.fsi`: Defines market-related types and their members.
*   `SelectionModels.fsi`: Defines selection-related types and their members.
*   `MarketExtensions.fsi`: Provides extension methods for market objects.
*   `SelectionExtensions.fsi`: Provides extension methods for selection objects.
*   `BotTriggerModels.fsi`: Defines types and interfaces related to bot triggers, including `BotTriggerParameters` and `TriggerResult`. When using `TriggerResult`, ensure you only use the cases explicitly defined within this `.fsi` file (e.g., `TriggerResult.EndExecution`, `TriggerResult.WaitingForOperation`, `TriggerResult.ExecuteActionBotOnSelection`).
*   `FootballMatchModels.fsi`: Specific models for football matches (if applicable).

## 3. Coding Style and Consistency

When developing new BotTrigger scripts or modifying existing ones, it is crucial to revisit and emulate the coding style, structure, and patterns present in other `*BotTrigger.fsx` files within the `src/Strategies` directory. This ensures consistency, readability, and maintainability across the codebase.

## 4. Basic F# BotTrigger Script Structure

A typical F# BotTrigger script will involve:
1.  **Opening necessary modules:** Importing required types and functions.
2.  **Defining the trigger logic:** Implementing conditions for bot actions, often using a state machine pattern (e.g., `TriggerStatus` discriminated union).
3.  **Performing actions:** Executing trading operations or logging information.

## 5. Your Bot Implementation Request

Use this section to clearly define your specific requirements for a new or modified BotTrigger script. Please provide details on the task, objective, core logic, and any specific termination conditions.