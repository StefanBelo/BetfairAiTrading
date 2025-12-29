# 🚀 Introducing Market Data Browser - A TradingView-Inspired Tool for Betfair Markets

Hey everyone! 👋

I've just finished building a web application that makes it much easier to analyze Betfair horse racing markets. Think of it as TradingView but specifically designed for Betfair data.

## 🎯 What It Does

The Market Data Browser is a React-based web app that pulls data from multiple sources and presents it in an easy-to-navigate interface. You can switch between different data contexts with just a click and see everything updated in real-time.

## 📊 Available Data Contexts

The app currently integrates four key data sources through a custom backend API:

### 1. **Timeform Data** (Table View)
- Horse form indicators (winner last time out, in form, beaten favorite)
- Suitability flags (going, course, distance)
- Trainer and jockey performance metrics
- All presented as boolean flags for quick analysis

### 2. **Racing Post Data** (Table View)
- Detailed race history for each horse
- Last 10+ races with full descriptions
- Statistics: days since last run, positions, beaten distances
- Weight carried and top speed metrics
- Aggregated averages across all races

### 3. **OLBG Tips Data** (Table View)
- Community tipster confidence ratings (0-100)
- Detailed tipster analysis and comments
- Pre-race assessments and reasoning
- Helps identify horses with strong backing from experienced tipsters

### 4. **Price History** (Interactive Charts)
- Historical price movements with TradingView-style charts
- Volume data displayed as histogram
- Full zoom/pan controls for detailed analysis
- See exactly when money came in or drifted out

## 🔧 Technical Stack

**Frontend:**
- React 18 + TypeScript
- Vite for lightning-fast builds
- AG-Grid for powerful data tables
- Lightweight Charts (TradingView library) for price visualization
- Zustand for state management

**API Integration:**
- Custom backend API (localhost:10043)
- REST endpoints for each data context
- Automatic price refresh when switching markets
- Response unwrapping for clean data structures

## 🎨 Key Features

✅ **Auto-refresh prices** - Click any market to fetch the latest odds  
✅ **Smart table formatting** - Race descriptions span full width with text wrapping  
✅ **Multiple view types** - Tables for comparative data, charts for trends  
✅ **Sortable/filterable** - AG-Grid gives you Excel-like data manipulation  
✅ **Responsive design** - Works on desktop, tablet, and mobile  

## 💡 How It Works

The app uses a simple but powerful workflow:

1. **Select a market** from the left sidebar (fetches fresh prices from API)
2. **Choose a data context** tab (Timeform, Racing Post, OLBG, or Charts)
3. **View the data** - Tables auto-populate, charts require runner selection
4. **Analyze** - Sort, filter, compare across all data sources

## 🛠️ API Endpoints Used

- `GET /api/getMonitoredMarkets` - Lists all available markets
- `GET /api/getMarket?marketId={id}` - Gets specific market with fresh prices
- `GET /api/getDataContextForMarket` - Fetches table data (Timeform, Racing Post, OLBG)
- `GET /api/getDataContextForMarketSelection` - Fetches chart data for specific runner

## 🎓 Use Cases

This tool is perfect for:
- **Pre-race analysis** - Compare all horses across multiple data sources in one view
- **Price monitoring** - Track how odds move leading up to a race
- **Form study** - Quickly identify horses with recent wins or consistent form
- **Tipster validation** - See which horses have strong community backing

## 📍 Project Location

The full project is in my GitHub repo under `/src/MarketDataBrowser`. It includes complete documentation, TypeScript interfaces, and all the transformers that shape the raw API data into usable tables.

## 🔮 What's Next?

Currently working on:
- Real-time WebSocket updates
- Export to CSV/Excel
- Multiple chart comparison view
- Dark theme support

---

Would love to hear your thoughts! Is this something you'd find useful? What other data sources would you like to see integrated?

**Tech stack geeks:** Happy to answer questions about the architecture, state management, or how the data transformers work!

🏇 Happy trading!
