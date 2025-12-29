# Market Data Browser

A professional web application for viewing and analyzing Betfair market data with support for both tabular and chart displays.

## Features

- **Market Browser**: Browse all monitored markets with search and filter
- **Auto-Refresh Prices**: Automatically fetches fresh market prices when selecting a market
- **Data Context Switching**: Seamlessly switch between different data contexts
- **Table Display**: View tabular data with sorting, filtering, and pagination (AG-Grid)
  - Text wrapping for description fields
  - Full-width display for race descriptions
  - Auto-height rows for multi-line content
- **Chart Display**: Interactive price history charts (Lightweight Charts by TradingView)
- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **TypeScript**: Fully typed for better development experience

## Technology Stack

- **React 18** with TypeScript
- **Vite** - Fast build tool
- **Zustand** - State management
- **Axios** - HTTP client
- **AG-Grid** - Data tables
- **Lightweight Charts** - Trading charts

## Getting Started

### Prerequisites

- Node.js 16+ and npm
- Backend API running on `http://localhost:10043`

### Installation

1. Install dependencies:
```bash
npm install
```

2. Start the development server:
```bash
npm run dev
```

The application will open at `http://localhost:3000`

### Build for Production

```bash
npm run build
```

## Project Structure

```
src/
├── components/          # React components
│   ├── MarketSelector.tsx
│   ├── DataContextSelector.tsx
│   ├── SelectionPicker.tsx
│   ├── TableDisplay.tsx
│   └── ChartDisplay.tsx
├── services/           # API service layer
│   └── api.ts
├── store/              # State management
│   └── useStore.ts
├── types/              # TypeScript types
│   └── index.ts
├── App.tsx             # Main application
├── main.tsx            # Entry point
└── index.css           # Global styles
```

## API Endpoints

The application connects to these API endpoints:

- `GET /api/getActiveMarket` - Get active market
- `GET /api/getMonitoredMarkets` - Get all monitored markets
- `GET /api/getMarket?marketId={id}` - Get specific market with fresh prices
- `GET /api/getDataContextForMarket` - Get market-level data (tables)
- `GET /api/getDataContextForMarketSelection` - Get selection-level data (charts)

## Available Data Contexts

- **TimeformDataForHorses** - Timeform ratings (Table)
- **RacingpostDataForHorses** - Racing Post data (Table)
- **OlbgRaceTipsData** - OLBG tips (Table)
- **MarketSelectionsPriceHistoryData** - Price history (Chart)

## License

See LICENSE file in the project root.
