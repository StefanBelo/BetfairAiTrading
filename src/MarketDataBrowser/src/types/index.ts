// Market and Selection types
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
  selectionName?: string; // Alias for compatibility
  odds?: number;
  price?: number; // API uses 'price' instead of 'odds'
  status?: string;
}

// Data Context types
export type DataContextType = 'table' | 'chart';

export interface DataContext {
  name: string;
  displayName: string;
  type: DataContextType;
  description: string;
}

// API Response types
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  error?: string;
}

// Table data - generic structure
export interface TableData {
  [key: string]: any;
}

// Chart data - price history
export interface PricePoint {
  time: number; // Unix timestamp
  price: number;
  volume?: number;
}

export interface ChartData {
  selectionId: string;
  selectionName: string;
  data: PricePoint[];
}

// Available data contexts
export const DATA_CONTEXTS: DataContext[] = [
  {
    name: 'TimeformDataForHorses',
    displayName: 'Timeform Data',
    type: 'table',
    description: 'Timeform ratings and statistics for horses'
  },
  {
    name: 'RacingpostDataForHorses',
    displayName: 'Racing Post Data',
    type: 'table',
    description: 'Racing Post data for horses'
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
