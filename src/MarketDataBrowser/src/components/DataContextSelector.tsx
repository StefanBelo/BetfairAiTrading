import React from 'react';
import { useStore } from '../store/useStore';
import { DATA_CONTEXTS } from '../types';
import './DataContextSelector.css';

export const DataContextSelector: React.FC = () => {
  const { selectedDataContext, selectDataContext, selectedMarket } = useStore();

  if (!selectedMarket) {
    return (
      <div className="alert alert-info d-flex align-items-center" role="alert">
        <i className="ti ti-info-circle me-2 fs-lg"></i>
        <span className="fw-medium">Select a market to view data contexts</span>
      </div>
    );
  }

  return (
    <div className="card">
      <div className="card-body p-0">
        <ul className="nav nav-tabs nav-bordered" role="tablist">
          {DATA_CONTEXTS.map((context, index) => (
            <li key={context.name} className="nav-item" role="presentation">
              <button
                className={`nav-link ${
                  selectedDataContext?.name === context.name ? 'active' : ''
                }`}
                type="button"
                role="tab"
                aria-selected={selectedDataContext?.name === context.name}
                onClick={() => selectDataContext(context)}
                title={context.description}
              >
                <span className="me-1">
                  {context.type === 'chart' ? '📈' : '📊'}
                </span>
                <span>{context.displayName}</span>
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};
