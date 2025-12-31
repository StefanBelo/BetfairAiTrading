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

  const filteredMarkets = markets
    .filter(market =>
      market.marketName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      market.eventName.toLowerCase().includes(searchTerm.toLowerCase())
    )
    .sort((a, b) => {
      const timeA = a.marketStartTime ? new Date(a.marketStartTime).getTime() : 0;
      const timeB = b.marketStartTime ? new Date(b.marketStartTime).getTime() : 0;
      return timeA - timeB;
    });

  const handleRefresh = () => {
    fetchMarkets();
    fetchActiveMarket();
  };

  return (
    <>
      {/* Markets Header */}
      <div className="px-3 py-3 border-bottom">
        <div className="d-flex justify-content-between align-items-center">
          <h4 className="mb-0 card-title">Markets</h4>
          <button 
            className="btn btn-sm btn-soft-primary" 
            onClick={handleRefresh}
            disabled={isLoading}
          >
            <i className="ti ti-refresh fs-18"></i>
          </button>
        </div>
      </div>

      {/* Search Box */}
      <div className="p-3">
        <div className="position-relative">
          <input
            type="text"
            className="form-control"
            placeholder="Search markets..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <i className="ti ti-search position-absolute top-50 end-0 translate-middle-y me-3 text-muted"></i>
        </div>
      </div>

      {/* Market List */}
      <ul className="side-nav">
        <li className="side-nav-title">Available Markets</li>
        {filteredMarkets.map((market) => (
          <li
            key={market.marketId}
            className="side-nav-item"
          >
            <a
              href="#"
              className={`side-nav-link ${
                selectedMarket?.marketId === market.marketId ? 'active' : ''
              }`}
              onClick={(e) => {
                e.preventDefault();
                selectMarket(market);
              }}
            >
              <span className="menu-text">
                <div className="fw-semibold">{market.eventName}</div>
                <div className="fs-sm text-muted">{market.marketName}</div>
                {market.marketStartTime && (
                  <div className="fs-xs text-muted mt-1">
                    {new Date(market.marketStartTime).toLocaleString()}
                  </div>
                )}
                {market.selections && (
                  <div className="fs-xs text-muted">
                    <i className="ti ti-users fs-sm me-1"></i>
                    {market.selections.length} selections
                  </div>
                )}
              </span>
              {activeMarket?.marketId === market.marketId && (
                <span className="badge bg-success">Active</span>
              )}
            </a>
          </li>
        ))}
        {filteredMarkets.length === 0 && (
          <li className="px-3 py-2 text-center text-muted">
            No markets found
          </li>
        )}
      </ul>
    </>
  );
};
