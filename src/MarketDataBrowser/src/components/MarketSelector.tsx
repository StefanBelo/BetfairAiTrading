import React, { useEffect, useState } from 'react';
import { useStore } from '../store/useStore';
import './MarketSelector.css';

export const MarketSelector: React.FC = () => {
  const { 
    markets, 
    selectedMarket, 
    activeMarket,
    fetchMarkets, 
    fetchActiveMarket,
    selectMarket,
    isLoading 
  } = useStore();

  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    fetchMarkets();
    fetchActiveMarket();
  }, []);

  const filteredMarkets = markets.filter(market =>
    market.marketName.toLowerCase().includes(searchTerm.toLowerCase()) ||
    market.eventName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleRefresh = () => {
    fetchMarkets();
    fetchActiveMarket();
  };

  return (
    <div className="market-selector">
      <div className="market-selector-header">
        <h2>Markets</h2>
        <button 
          className="refresh-button" 
          onClick={handleRefresh}
          disabled={isLoading}
        >
          {isLoading ? '...' : '↻'}
        </button>
      </div>

      <div className="search-box">
        <input
          type="text"
          placeholder="Search markets..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>

      <div className="market-list">
        {filteredMarkets.map((market) => (
          <div
            key={market.marketId}
            className={`market-item ${
              selectedMarket?.marketId === market.marketId ? 'selected' : ''
            } ${
              activeMarket?.marketId === market.marketId ? 'active' : ''
            }`}
            onClick={() => selectMarket(market)}
          >
            <div className="market-event-name">{market.eventName}</div>
            <div className="market-name">{market.marketName}</div>
            {market.marketStartTime && (
              <div className="market-time">
                {new Date(market.marketStartTime).toLocaleString()}
              </div>
            )}
            {market.selections && (
              <div className="market-selections">
                {market.selections.length} selections
              </div>
            )}
            {activeMarket?.marketId === market.marketId && (
              <span className="active-badge">Active</span>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};
