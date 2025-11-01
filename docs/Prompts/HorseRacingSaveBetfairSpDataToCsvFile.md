# Horse Racing Save BetfairSpData to CSV File

## Step 1: Retrieve Active Market Data
Call `GetActiveMarket` (no parameters) to get the `marketId` and selections (horses) with their current prices.

## Step 2: Retrieve BetfairSpData for All Selections
Call `GetDataContextForMarket` with:
- `dataContextName`: "BetfairSpData"
- `marketId`: (from Step 1)

This returns, for each selection:
- `industryStartingPrice`: Traditional bookmaker forecast price
- `price`: Current market price (from BetfairSpData context)
- `betfairSP`: Betfair Starting Price (predicted or actual)

## Step 3: Present Results
For each race, present a table with the following columns showing **only the horses from the current active market**:
- Selection ID
- Horse Name
- Current Price
- Industry SP (industryStartingPrice)
- Betfair SP (betfairSP)

**Instructions:**
- Sort all horses by Horse Name alphabetically.
- Display the table in this sorted order.
- Save the table data to a CSV file named `BetfairSpData.csv` in the `data/` directory.
- **Row Update Logic:**
  - Read the existing CSV file (if it exists)
  - For each horse in the current market data:
    - If the Selection ID already exists in the CSV, update that specific row with the new data
    - If the Selection ID does not exist, append a new row
  - Maintain the alphabetical sort order by Horse Name in the final CSV
  - Preserve any existing horses from previous markets that are not in the current market
- **Execution Note:** The AI agent should execute these operations internally using its available tools, without generating or running external Python code. Use file reading/writing tools to update the CSV directly.

**Example Table Format:**
| Selection ID       | Horse Name         | Current Price | Industry SP | Betfair SP |
|--------------------|--------------------|--------------|-------------|------------|
| [Selection ID]     | [Horse 1]          | [Price]      | [Ind SP]    | [BSP]      |
| [Selection ID]     | [Horse 2]          | [Price]      | [Ind SP]    | [BSP]      |
| ...                | ...                | ...          | ...         | ...        |

---
*This prompt is optimized for Bfexplorer MCP tools to save BetfairSpData to CSV for further analysis.*