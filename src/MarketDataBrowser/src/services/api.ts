import axios from 'axios';
import { Market, TableData, ChartData } from '../types';

const API_BASE_URL = '/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 60000, // 60 seconds for slow endpoints
  headers: {
    'Content-Type': 'application/json',
  },
});

// API service methods
export const marketApi = {
  // Get the currently active market
  getActiveMarket: async (): Promise<Market> => {
    const response = await apiClient.get('/getActiveMarket');
    return response.data.activeBetfairMarket || response.data;
  },

  // Get all monitored markets
  getMonitoredMarkets: async (): Promise<Market[]> => {
    const response = await apiClient.get('/getMonitoredMarkets');
    return response.data;
  },

  // Get a specific market by ID
  getMarket: async (marketId: string): Promise<Market> => {
    const response = await apiClient.get('/getMarket', {
      params: { marketId },
    });
    
    // Unwrap the response if needed
    let market = response.data;
    if (response.data.market) {
      market = response.data.market;
    } else if (response.data.betfairMarket) {
      market = response.data.betfairMarket;
    }
    
    return market;
  },

  // Get market-level data context (for tables)
  getDataContextForMarket: async (
    dataContextName: string,
    marketId: string
  ): Promise<any> => {
    const response = await apiClient.get('/getDataContextForMarket', {
      params: { dataContextName, marketId },
    });
    // API returns: { market: {...}, selectionsData: [...] }
    return response.data;
  },

  // Get selection-level data context (for charts)
  getDataContextForMarketSelection: async (
    dataContextName: string,
    marketId: string,
    selectionId: string
  ): Promise<any> => {
    try {
      console.log('Fetching chart data:', { dataContextName, marketId, selectionId });
      const response = await apiClient.get('/getDataContextForMarketSelection', {
        params: { dataContextName, marketId, selectionId },
        timeout: 60000, // Extended timeout for chart data
      });
      console.log('Chart data response:', response.data);
      // API returns: { market: {...}, selection: {...}, data: [...] }
      return response.data;
    } catch (error: any) {
      console.error('Error fetching chart data:', error.message, error.response?.data);
      throw error;
    }
  },
};

// Error handling interceptor
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('API Error:', error);
    return Promise.reject(error);
  }
);

export default apiClient;
