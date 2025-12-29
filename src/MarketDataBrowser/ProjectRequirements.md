# Market Data Browser - Project Requirements & Implementation

## 1. Project Overview

The Market Data Browser is a web application inspired by TradingView that provides a comprehensive interface for viewing and analyzing Betfair market data. The application dynamically displays data in appropriate formats (tables or charts) based on the data context type.

**Project Location:** `E:\Projects\BetfairAiTrading\src\MarketDataBrowser`

**Backend API Base URL:** `http://localhost:10043/api`

**Status:** ✅ Fully Implemented

---

## 2. Project Structure

```
src/MarketDataBrowser/
├── public/
├── src/
│   ├── components/
│   │   ├── MarketSelector.tsx        # Left sidebar with market list
│   │   ├── MarketSelector.css
│   │   ├── DataContextSelector.tsx   # Tab interface for data contexts
│   │   ├── DataContextSelector.css
│   │   ├── SelectionPicker.tsx       # Dropdown for chart selections
│   │   ├── SelectionPicker.css
│   │   ├── TableDisplay.tsx          # AG-Grid table component
│   │   ├── TableDisplay.css
│   │   ├── ChartDisplay.tsx          # Lightweight Charts component
│   │   └── ChartDisplay.css
│   ├── services/
│   │   └── api.ts                    # Axios API client
│   ├── store/
│   │   └── useStore.ts              # Zustand state management + data transformers
│   ├── types/
│   │   └── index.ts                 # TypeScript interfaces
│   ├── App.tsx                      # Main app layout
│   ├── App.css
│   ├── main.tsx                     # Entry point
│   └── index.css                    # Global styles
├── index.html
├── package.json
├── tsconfig.json
├── tsconfig.node.json
├── vite.config.ts
├── .gitignore
└── README.md
```

## 2. Data Requirements

### 2.1 API Endpoints

#### 2.1.1 Market Data Endpoints

| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/getActiveMarket` | GET | Retrieves the currently active market | None |
| `/api/getMonitoredMarkets` | GET | Retrieves all available monitored markets | None |
| `/api/getMarket` | GET | Retrieves a specific market by ID | `marketId` |

**Example Request:**
```
GET http://localhost:10043/api/getActiveMarket
GET http://localhost:10043/api/getMonitoredMarkets
GET http://localhost:10043/api/getMarket?marketId=1.252101495
```

**Response Structure (getMonitoredMarkets):**
```json
[
  {
    "marketId": "1.252073388",
    "startTime": "2025-12-28T16:50:00Z",
    "eventType": "Horse Racing",
    "eventName": "Southwell",
    "marketName": "1m3f Hcap",
    "status": "Open",
    "selections": [
      {
        "selectionId": "70465716_0.00",
        "name": "Mr Nugget",
        "price": 4.8,
        "status": "Active"
      }
    ]
  }
]
```

#### 2.1.2 Data Context Endpoints

##### Market-Level Data Context
Used for tabular data across all selections in a market.

| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/getDataContextForMarket` | GET | Retrieves market-level context data | `dataContextName`, `marketId` |

**Example Request:**
```
GET http://localhost:10043/api/getDataContextForMarket?dataContextName=TimeformDataForHorses&marketId=1.252073388
```

**Response Structure:**
```json
{
  "market": {
    "marketId": "1.252073388",
    "startTime": "2025-12-28T17:50:00+01:00",
    "eventType": "Horse Racing",
    "eventName": "Southwell",
    "marketName": "1m3f Hcap",
    "status": "Open"
  },
  "selectionsData": [
    {
      "selectionId": "74399372_0.00",
      "name": "Stipulation",
      "data": {
        "timeformHorseData": {
          "horseWinnerLastTimeOut": false,
          "horseInForm": false,
          "horseBeatenFavouriteLTO": false,
          "suitedByGoing": true,
```        
Transformation:** Data is transformed using context-specific transformers

5. **Data Rendering:** Application displays data in appropriate format (table or chart)

### 2.4 Implemented Data Transformers

All transformers are located in `src/store/useStore.ts` and merge market selection prices with context data.

#### 2.4.1 TimeformDataForHorses Transformer
```typescript
const transformTimeformData = (selectionsData: any[], marketSelections: Selection[]): TableData[] => {
  return selectionsData.map(item => {
    const marketSelection = marketSelections.find(s => s.selectionId === item.selectionId);
    return {
      name: item.name,
      price: marketSelection?.price || marketSelection?.odds || 'N/A',
      ...item.data?.timeformHorseData || {}
    };
  });
};
```
**Output Columns:** name, price, horseWinnerLastTimeOut, horseInForm, horseBeatenFavouriteLTO, suitedByGoing, suitedByCourse, suitedByDistance, trainerInForm, trainerCourseRecord, jockeyInForm, jockeyWonOnHorse, timeformTopRated, timeformImprover, timeformHorseInFocus

**Timeform Data Structure:**
```json
{
  "timeformHorseData": {
    "horseWinnerLastTimeOut": false,
    "horseInForm": true,
    "horseBeatenFavouriteLTO": false,
    "suitedByGoing": true,
    "suitedByCourse": false,
    "suitedByDistance": true,
    "trainerInForm": false,
    "trainerCourseRecord": false,
    "jockeyInForm": true,
    "jockeyWonOnHorse": false,
    "timeformTopRated": false,
    "timeformImprover": false,
    "timeformHorseInFocus": false
  }
}
```

#### 2.4.2 RacingpostDataForHorses Transformer
```typescript
const transformRacingpostData = (selectionsData: any[], marketSelections: Selection[]): TableData[] => {
  return selectionsData.map(item => {
    const horseData = item.data?.racingpostHorseData || {};
    const lastRaces = horseData.lastRaces || horseData.lastRacesDescriptions || [];
    const marketSelection = marketSelections.find(s => s.selectionId === item.selectionId);
    const lastRace = lastRaces.length > 0 ? lastRaces[0] : null;
    const totalRaces = lastRaces.length;
    
    return {
      name: item.name,
      price: marketSelection?.price || marketSelection?.odds || 'N/A',
      totalRaces,
      lastRunDays: lastRace?.lastRunInDays || 'N/A',
      lastPosition: lastRace?.position || 'N/A',
      lastDistance: lastRace?.distance || 'N/A',
      lastBeatenDist: lastRace?.beatenDistance || 'N/A',
      lastWeight: lastRace?.weightCarried || 'N/A',
      lastTopSpeed: lastRace?.topspeed || 'N/A',
      avgBeatenDistance: /* calculated */,
      avgPosition: /* calculated */,
      lastRaceDesc: lastRace?.raceDescription || 'N/A',
    };
  });
};
```
**Output Columns:** name, price, totalRaces, lastRunDays, lastPosition, lastDistance, lastBeatenDist, lastWeight, lastTopSpeed, avgBeatenDistance, avgPosition, lastRaceDesc

**Racing Post Data Structure:**
```json
{
  "racingpostHorseData": {
    "lastRaces": [
      {
        "lastRunInDays": 33,
        "position": 0,
        "distance": 8641,
        "beatenDistance": 0,
        "weightCarried": 149,
        "topspeed": 0,
        "raceDescription": "coughing"
      }
    ]
  }
}
```

#### 2.4.3 OlbgRaceTipsData Transformer
```typescript
const transformOlbgData = (selectionsData: any[], marketSelections: Selection[]): TableData[] => {
  return selectionsData.map(item => {
    const tipsData = item.data?.olbgRaceTipData || {};
    const marketSelection = marketSelections.find(s => s.selectionId === item.selectionId);
    return {
      name: item.name,
      price: marketSelection?.price || marketSelection?.odds || 'N/A',
      confidence: tipsData.confidence || 0,
      comments: tipsData.comments || '',
      preraceComment: tipsData.preraceComment || '',
      ...tipsData
    };
  });
};
```
**Output Columns:** name, price, confidence, comments, preraceComment, plus all fields from olbgRaceTipData

**OLBG Data Structure:**
```json
{
  "olbgRaceTipData": {
    "comments": "Detailed tipster analysis and reasoning",
    "confidence": 52,
    "preraceComment": "Pre-race analysis and expectations"
  }
}
```

##### Selection-Level Data Context
Used for time-series and selection-specific data.

| Endpoint | Method | Description | Parameters |
|----------|--------|-------------|------------|
| `/api/getDataContextForMarketSelection` | GET | Retrieves selection-level context data | `dataContextName`, `marketId`, `selectionId` |

**Example Request:**
```
GET http://localhost:10043/api/getDataContextForMarketSelection?dataContextName=MarketSelectionsPriceHistoryData&marketId=1.252073388&selectionId=70465716_0.00
```

**Response Structure:**
```json
{
  "market": { "marketId": "1.252073388", ... },
  "selectionsData": [
    {
      "selectionId": "70465716_0.00",
      "name": "Mr Nugget",
      "data": {
        "timePriceVolumes": [
          {
            "time": "2025-12-28T16:52:14+01:00",
            "price": 5.2,
            "volume": 7.44
          }
        ]
      }
    }
  ]
}
```

### 2.2 Data Context Types

#### 2.2.1 Time-Series Data (Chart Display)

| Data Context Name | Endpoint Type | Display Format | Description |
|-------------------|---------------|----------------|-------------|
| `MarketSelectionsPriceHistoryData` | Selection-Level | Line Chart / Candlestick | Historical price movements for a specific selection |

**Retrieval Pattern:**
```
/api/getDataContextForMarketSelection?dataContextName=MarketSelectionsPriceHistoryData&marketId={marketId}&selectionId={selectionId}
```

#### 2.2.2 Tabular Data (Table Display)

| Data Context Name | Endpoint Type | Display Format | Description |
|-------------------|---------------|----------------|-------------|
| `TimeformDataForHorses` | Market-Level | Data Table | Timeform ratings and statistics for horses |
| `RacingpostDataForHorses` | Market-Level | Data Table | Racing Post data for horses |
| `OlbgRaceTipsData` | Market-Level | Data Table | OLBG community racing tips |

**Retrieval Pattern:**
```
/api/getDataContextForMarket?dataContextName={contextName}&marketId={marketId}
```

### 2.3 Data Flow

1. **Market Selection:** User selects from active or monitored markets
2. **Context Selection:** User chooses which data context to view
3. **Data Retrieval:** Application fetches appropriate data based on context type
4. **Data Rendering:** Application displays data in appropriate format (table or chart)

---

## 3. User Interface Requirements

### 3.1 Layout Structure

The application should follow a TradingView-inspired layout with the following components:

```
┌─────────────────────────────────────────────────────────────┐
│ Header / Navigation                                          │
├──────────────┬──────────────────────────────────────────────┤
│              │                                               │
│  Market      │           Main Data Display Area             │
│  Selector    │                                               │
│              │    (Charts or Tables based on context)        │
│  Data        │                                               │
│  Context     │                                               │
│  Selector    │                                               │
│              │                                               │
└──────────────┴──────────────────────────────────────────────┘
```

### 3.2 Component Requirements

#### 3.2.1 Market Selector Panel (Left Sidebar)

**Purpose:** Allow users to browse and select markets

**Features:**
- Display all monitored markets in a list/tree view
- Highlight the currently active market
- Show market metadata:
  - Market name
  - Market ID
  - Event name
  - Market start time
  - Number of selections
- Search/filter functionality for markets
- Refresh button to reload market list

**Interaction:**
- Clicking a market loads its selections and available data contexts

#### 3.2.2 Data Context Selector

**Purpose:** Allow users to choose which data view to display

**Features:**
- Dropdown or tab interface showing available data contexts:
  - TimeformDataForHorses
  - RacingpostDataForHorses
  - OlbgRaceTipsData
  - MarketSelectionsPriceHistoryData
- Visual indicator for data type (📊 Table / 📈 Chart)
- Description tooltip for each context type

---

## 3. User Interface Requirements

### 3.1 Layout Structure

The application follows a TradingView-inspired layout with these components:

```
┌─────────────────────────────────────────────────────────────┐
│ Header / Navigation                                          │
├──────────────┬──────────────────────────────────────────────┤
│              │                                               │
│  Market      │           Main Data Display Area             │
│  Selector    │                                               │
│              │    (Charts or Tables based on context)        │
│  Data        │                                               │
│  Context     │                                               │
│  Selector    │                                               │
│              │                                               │
└──────────────┴──────────────────────────────────────────────┘
```

### 3.2 Component Requirements

#### 3.2.1 Market Selector Panel (Left Sidebar)

**Features:**
- Display all monitored markets in a list
- Highlight currently active market
- Show market metadata (name, ID, event, start time, selections)
- Search/filter functionality
- Refresh button

#### 3.2.2 Data Context Selector

**Features:**
- Tab interface showing available data contexts
- Visual indicator for data type (📊 Table / 📈 Chart)
- Description tooltip for each context

#### 3.2.3 Selection Picker (For Chart Views Only)

**Features:**
- Dropdown or list of all selections
- Selection details (name, ID, odds)
- Quick selection switching

#### 3.2.4 Main Data Display Area

**Table Display Features:**
- Sortable, filterable columns
- Responsive design
- Pagination for large datasets
- Export functionality

**Chart Display Features:**
- Interactive line chart
- Zoom and pan controls
- Time range selector
- Crosshair with price/time info
- Responsive sizing

### 3.3 User Workflows

#### 3.3.1 View Tabular Data
1. Select market from market selector
2. Choose tabular context (e.g., RacingpostDataForHorses)
3. Application fetches via `/api/getDataContextForMarket`
4. Data displayed in sortable, filterable table

#### 3.3.2 View Chart Data
1. Select market
2. Choose MarketSelectionsPriceHistoryData context
3. Selection picker appears
4. Select specific runner
5. Application fetches via `/api/getDataContextForMarketSelection`
6. Price history displayed as chart

---

## 4. Technical Implementation

### 4.1 Technology Stack (Implemented)

- **Language:** TypeScript 5.2.2
- **Frontend Framework:** React 18.2.0
- **Build Tool:** Vite 5.0.8
- **Charting Library:** lightweight-charts 4.1.1 (TradingView)
- **Table Component:** ag-grid-react 31.0.1 + ag-grid-community 31.0.1
- **HTTP Client:** Axios 1.6.2
- **State Management:** Zustand 4.4.7
- **Styling:** Plain CSS with CSS variables

**Installed Dependencies:**
```json
{
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "zustand": "^4.4.7",
    "axios": "^1.6.2",
    "ag-grid-react": "^31.0.1",
    "ag-grid-community": "^31.0.1",
    "lightweight-charts": "^4.1.1"
  },
  "devDependencies": {
    "@types/react": "^18.2.43",
    "@types/react-dom": "^18.2.17",
    "@vitejs/plugin-react": "^4.2.1",
    "typescript": "^5.2.2",
    "vite": "^5.0.8"
  }
}
```

### 4.2 Key Implementation Details

#### 4.2.1 API Service (src/services/api.ts)
- Axios instance with 60-second timeout
- Base URL: `/api` (proxied to localhost:10043)
- Exports `marketApi` object with 5 methods:
  - `getActiveMarket()` - Get currently active market
  - `getMonitoredMarkets()` - Get all monitored markets
  - `getMarket(marketId)` - Get specific market with fresh prices (unwraps response)
  - `getDataContextForMarket(contextName, marketId)` - Get market-level data
  - `getDataContextForMarketSelection(contextName, marketId, selectionId)` - Get selection data
- Automatic response unwrapping for different API response structures
- Error handling with console logging

#### 4.2.2 State Management (src/store/useStore.ts)
- Zustand store with TypeScript
- State: markets, selectedMarket, selectedDataContext, selectedSelection, tableData, chartData, isLoading, error
- Actions: fetchMarkets, selectMarket, selectDataContext, selectSelection, fetchTableData, fetchChartData
- Data transformers for each context type
- Auto-fetches data when context/selection changes

#### 4.2.3 Table Display (src/components/TableDisplay.tsx)
- AG-Grid with auto-generated columns
- Column headers converted from camelCase to Title Case
- Boolean values displayed as ✓/✗ symbols
- Name column pinned left (150px width)
- Sortable, filterable, resizable columns
- Pagination (50 rows per page)
- Smart text wrapping for comment/description fields
- Full-width display for race description fields (spans all columns)
- Auto-height rows for multi-line content
- Special styling for description cells (light gray background, italic text)

#### 4.2.4 Chart Display (src/components/ChartDisplay.tsx)
- Lightweight Charts library
- Extracts `selectionsData[0].data.timePriceVolumes`
- Converts ISO timestamps to Unix seconds
- Removes duplicate timestamps (keeps highest price, sums volumes)
- **Price Chart:** Line chart showing price movements (top 70% of chart)
- **Volume Chart:** Histogram/bar chart showing volume (bottom 30% of chart)
- Chart height: 500px
- Auto-fits time scale
- ResizeObserver for responsive width
- Both series share the same time axis

#### 4.2.5 Component Interaction Flow
1. MarketSelector loads markets on mount
2. User clicks market → `selectMarket()` called
   - Calls `getMarket` API to fetch fresh prices
   - Updates selectedMarket with fresh data
   - Auto-refreshes current data context if one is selected
   - Falls back to cached data if API call fails
3. User clicks data context tab → `selectDataContext()` called
4. For table contexts: `fetchTableData()` auto-called with fresh marketId
5. For chart context: SelectionPicker appears
6. User selects runner → `selectSelection()` → `fetchChartData()` called
7. Data transformed and displayed with current prices

### 4.3 TypeScript Interfaces (src/types/index.ts)

```typescript
export interface Market {
  marketId: string;
  marketName: string;
  eventName: string;
  marketStartTime?: string;
  selections: Selection[];
  status?: string;
}

export interface Selection {
  selectionId: string;
  name: string;
  selectionName?: string;
  odds?: number;
  price?: number;
  status?: string;
}

export interface DataContext {
  name: string;
  displayName: string;
  type: DataContextType;
  description: string;
}

export type DataContextType = 'table' | 'chart';

export interface TableData {
  [key: string]: any;
}

export const DATA_CONTEXTS: DataContext[] = [
  {
    name: 'TimeformDataForHorses',
    displayName: 'Timeform Data',
    type: 'table',
    description: 'Timeform ratings and statistics'
  },
  {
    name: 'RacingpostDataForHorses',
    displayName: 'Racing Post Data',
    type: 'table',
    description: 'Racing Post race history and statistics'
  },
  {
    name: 'OlbgRaceTipsData',
    displayName: 'OLBG Tips',
    type: 'table',
    description: 'OLBG community racing tips'
  },
  {
    name: 'MarketSelectionsPriceHistoryData',
    displayName: 'Price History',
    type: 'chart',
    description: 'Historical price movements'
  }
];
```

### 4.4 Running the Application

**Development Server:**
```bash
cd E:\Projects\BetfairAiTrading\src\MarketDataBrowser
npm install
npm run dev
```
Application runs on: http://localhost:3000

**Prerequisites:**
- Node.js installed
- Backend API running on http://localhost:10043

### 4.5 Future Enhancements (Not Implemented)

- Real-time data updates via WebSocket
- Multiple chart views side-by-side
- Custom data filtering and alerts
- Save user preferences and layouts
- Export charts as images
- Historical data playback
- CSV/Excel export for tables
- Dark theme toggle

---

## 5. Configuration Files

### 5.1 vite.config.ts
### 5.1 vite.config.ts
```typescript
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    proxy: {
      '/api': {
        target: 'http://localhost:10043',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '/api'),
        configure: (proxy, _options) => {
          proxy.on('error', (err, _req, _res) => {
            console.log('proxy error', err);
          });
          proxy.on('proxyReq', (proxyReq, req, _res) => {
            console.log('Sending Request to the Target:', req.method, req.url);
          });
          proxy.on('proxyRes', (proxyRes, req, _res) => {
            console.log('Received Response from the Target:', proxyRes.statusCode, req.url);
          });
        },
      }
    }
  }
})
```

### 5.2 tsconfig.json
```json
{
  "compilerOptions": {
    "target": "ES2020",
    "useDefineForClassFields": true,
    "lib": ["ES2020", "DOM", "DOM.Iterable"],
    "module": "ESNext",
    "skipLibCheck": true,
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "resolveJsonModule": true,
    "isolatedModules": true,
    "noEmit": true,
    "jsx": "react-jsx",
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true
  },
  "include": ["src"],
  "references": [{ "path": "./tsconfig.node.json" }]
}
```

---

## 6. Data Availability Notes

**Current Status:**
- ✅ **TimeformDataForHorses**: Returns valid data with boolean flags for horse form indicators
- ✅ **RacingpostDataForHorses**: Returns valid data from active horse racing markets with race history
- ✅ **OlbgRaceTipsData**: Available when OLBG tips exist for the market (confidence, comments, preraceComment)
- ✅ **MarketSelectionsPriceHistoryData**: Available for all active markets with price/volume history

**Note for Developers:** All four data contexts are now fully operational. Data availability depends on the backend API and current market conditions. Each data context returns comprehensive information for making informed betting decisions.