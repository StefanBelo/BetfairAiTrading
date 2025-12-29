import { create } from 'zustand';
import { Market, Selection, DataContext, TableData } from '../types';
import { marketApi } from '../services/api';

// Data transformers for different context types
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

const transformRacingpostData = (selectionsData: any[], marketSelections: Selection[]): TableData[] => {
  return selectionsData.map(item => {
    const horseData = item.data?.racingpostHorseData || {};
    const lastRaces = horseData.lastRaces || horseData.lastRacesDescriptions || [];
    const marketSelection = marketSelections.find(s => s.selectionId === item.selectionId);
    
    // Get most recent race (first in array)
    const lastRace = lastRaces.length > 0 ? lastRaces[0] : null;
    
    // Calculate aggregated statistics from all races
    const totalRaces = lastRaces.length;
    const avgBeatenDistance = totalRaces > 0 
      ? (lastRaces.reduce((sum: number, race: any) => sum + (race.beatenDistance || 0), 0) / totalRaces).toFixed(2)
      : 'N/A';
    const avgPosition = totalRaces > 0
      ? (lastRaces.reduce((sum: number, race: any) => sum + (race.position || 0), 0) / totalRaces).toFixed(1)
      : 'N/A';
    
    // Flatten the data structure with aggregated stats and last race details
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
      avgBeatenDistance,
      avgPosition,
      lastRaceDesc: lastRace?.raceDescription || 'N/A',
    };
  });
};

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

const getDataTransformer = (dataContextName: string) => {
  switch (dataContextName) {
    case 'TimeformDataForHorses':
      return transformTimeformData;
    case 'RacingpostDataForHorses':
      return transformRacingpostData;
    case 'OlbgRaceTipsData':
      return transformOlbgData;
    default:
      // Default transformer: flatten data and add name
      return (selectionsData: any[], marketSelections: Selection[]) => 
        selectionsData.map(item => {
          const marketSelection = marketSelections.find(s => s.selectionId === item.selectionId);
          return {
            name: item.name,
            price: marketSelection?.price || marketSelection?.odds || 'N/A',
            ...item.data || {}
          };
        });
  }
};

interface AppState {
  // Markets
  markets: Market[];
  selectedMarket: Market | null;
  activeMarket: Market | null;
  
  // Data Context
  selectedDataContext: DataContext | null;
  selectedSelection: Selection | null;
  
  // Data
  tableData: TableData[];
  chartData: any;
  
  // UI State
  isLoading: boolean;
  error: string | null;
  
  // Actions
  fetchMarkets: () => Promise<void>;
  fetchActiveMarket: () => Promise<void>;
  selectMarket: (market: Market) => Promise<void>;
  selectDataContext: (context: DataContext) => void;
  selectSelection: (selection: Selection) => void;
  fetchTableData: (dataContextName: string, marketId: string) => Promise<void>;
  fetchChartData: (dataContextName: string, marketId: string, selectionId: string) => Promise<void>;
  setError: (error: string | null) => void;
}

export const useStore = create<AppState>((set, get) => ({
  // Initial state
  markets: [],
  selectedMarket: null,
  activeMarket: null,
  selectedDataContext: null,
  selectedSelection: null,
  tableData: [],
  chartData: null,
  isLoading: false,
  error: null,

  // Fetch all monitored markets
  fetchMarkets: async () => {
    set({ isLoading: true, error: null });
    try {
      const markets = await marketApi.getMonitoredMarkets();
      set({ markets, isLoading: false });
    } catch (error: any) {
      set({ error: error.message, isLoading: false });
    }
  },

  // Fetch active market
  fetchActiveMarket: async () => {
    set({ isLoading: true, error: null });
    try {
      const activeMarket = await marketApi.getActiveMarket();
      set({ activeMarket, isLoading: false });
    } catch (error: any) {
      set({ error: error.message, isLoading: false });
    }
  },

  // Select a market
  selectMarket: async (market: Market) => {
    set({ isLoading: true });
    
    try {
      // Fetch fresh market data with updated prices
      const freshMarket = await marketApi.getMarket(market.marketId);
      
      set({ 
        selectedMarket: freshMarket,
        selectedSelection: null,
        tableData: [],
        chartData: null,
        isLoading: false
      });
      
      // Auto-refresh current data context if one is selected
      const { selectedDataContext } = get();
      if (selectedDataContext && selectedDataContext.type === 'table') {
        get().fetchTableData(selectedDataContext.name, freshMarket.marketId);
      }
    } catch (error: any) {
      console.error('Error fetching fresh market, using cached:', error);
      // Fallback to using the cached market data
      set({ 
        selectedMarket: market,
        selectedSelection: null,
        tableData: [],
        chartData: null,
        isLoading: false
      });
    }
  },

  // Select a data context
  selectDataContext: (context: DataContext) => {
    set({ 
      selectedDataContext: context,
      selectedSelection: null,
      tableData: [],
      chartData: null
    });

    // Auto-fetch table data if context is table type
    const { selectedMarket } = get();
    if (context.type === 'table' && selectedMarket) {
      get().fetchTableData(context.name, selectedMarket.marketId);
    }
  },

  // Select a selection (for chart data)
  selectSelection: (selection: Selection) => {
    set({ selectedSelection: selection, chartData: null }); // Clear old chart data

    // Auto-fetch chart data
    const { selectedMarket, selectedDataContext } = get();
    if (selectedMarket && selectedDataContext && selectedDataContext.type === 'chart') {
      get().fetchChartData(
        selectedDataContext.name,
        selectedMarket.marketId,
        selection.selectionId
      );
    }
  },

  // Fetch table data
  fetchTableData: async (dataContextName: string, marketId: string) => {
    set({ isLoading: true, error: null });
    try {
      const response = await marketApi.getDataContextForMarket(dataContextName, marketId);
      // API returns: { market: {...}, selectionsData: [...] }
      const selectionsData = response.selectionsData || [];
      
      // Get market selections with prices from the selected market
      const { selectedMarket } = get();
      const marketSelections = selectedMarket?.selections || [];
      
      // Transform the data based on context type
      const transformer = getDataTransformer(dataContextName);
      const transformedData = transformer(selectionsData, marketSelections);
      
      set({ tableData: transformedData, isLoading: false });
    } catch (error: any) {
      set({ error: error.message, isLoading: false, tableData: [] });
    }
  },

  // Fetch chart data
  fetchChartData: async (dataContextName: string, marketId: string, selectionId: string) => {
    set({ isLoading: true, error: null });
    try {
      const response = await marketApi.getDataContextForMarketSelection(
        dataContextName,
        marketId,
        selectionId
      );
      // API returns: { market: {...}, selection: {...}, data: [...] }
      set({ chartData: response, isLoading: false });
    } catch (error: any) {
      set({ error: error.message, isLoading: false, chartData: null });
    }
  },

  // Set error
  setError: (error: string | null) => {
    set({ error });
  },
}));
