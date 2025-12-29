import React from 'react';
import { useStore } from '../store/useStore';
import { DATA_CONTEXTS } from '../types';
import './DataContextSelector.css';

export const DataContextSelector: React.FC = () => {
  const { selectedDataContext, selectDataContext, selectedMarket } = useStore();

  if (!selectedMarket) {
    return (
      <div className="data-context-selector">
        <p className="no-market-message">Select a market to view data contexts</p>
      </div>
    );
  }

  return (
    <div className="data-context-selector">
      <h3>Data Context</h3>
      <div className="context-tabs">
        {DATA_CONTEXTS.map((context) => (
          <button
            key={context.name}
            className={`context-tab ${
              selectedDataContext?.name === context.name ? 'active' : ''
            }`}
            onClick={() => selectDataContext(context)}
            title={context.description}
          >
            <span className="context-icon">
              {context.type === 'chart' ? '📈' : '📊'}
            </span>
            <span className="context-label">{context.displayName}</span>
          </button>
        ))}
      </div>
    </div>
  );
};
