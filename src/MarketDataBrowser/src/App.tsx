import React from 'react';
import { MainLayout } from './components/layout/MainLayout';
import { MarketSelector } from './components/MarketSelector';
import { DataContextSelector } from './components/DataContextSelector';
import { SelectionPicker } from './components/SelectionPicker';
import { TableDisplay } from './components/TableDisplay';
import { ChartDisplay } from './components/ChartDisplay';
import { useStore } from './store/useStore';

function App() {
  const { selectedDataContext, selectedMarket, error } = useStore();

  return (
    <MainLayout sidebar={<MarketSelector />}>
      {/* Page Title */}
      <div className="row">
        <div className="col-12">
          <div className="page-title-box">
            {selectedMarket && (
              <>
                <h4 className="page-title mb-2">
                  {selectedMarket.eventName} - {selectedMarket.marketName}
                </h4>
                {selectedMarket.marketStartTime && (
                  <p className="text-muted mb-2 fs-sm">
                    <i className="ti ti-clock me-1"></i>
                    {new Date(selectedMarket.marketStartTime).toLocaleString()}
                  </p>
                )}
              </>
            )}
            <nav aria-label="breadcrumb">
              <ol className="breadcrumb m-0">
                <li className="breadcrumb-item"><a href="#">Betfair</a></li>
                {selectedMarket ? (
                  <>
                    <li className="breadcrumb-item">{selectedMarket.eventName}</li>
                    <li className="breadcrumb-item active" aria-current="page">{selectedMarket.marketName}</li>
                  </>
                ) : (
                  <li className="breadcrumb-item active" aria-current="page">Markets</li>
                )}
              </ol>
            </nav>
          </div>
        </div>
      </div>

      {/* Data Context Selector */}
      <div className="row">
        <div className="col-12">
          <DataContextSelector />
        </div>
      </div>

      {/* Selection Picker for Charts */}
      {selectedDataContext?.type === 'chart' && (
        <div className="row mt-3">
          <div className="col-12">
            <SelectionPicker />
          </div>
        </div>
      )}

      {/* Error Alert */}
      {error && (
        <div className="row mt-3">
          <div className="col-12">
            <div className="alert alert-danger alert-dismissible fade show" role="alert">
              <i className="ti ti-alert-circle me-2"></i>
              <strong>Error:</strong> {error}
              <button type="button" className="btn-close" aria-label="Close"></button>
            </div>
          </div>
        </div>
      )}

      {/* Data Display */}
      <div className="row mt-3">
        <div className="col-12">
          {!selectedDataContext && (
            <div className="card">
              <div className="card-body text-center p-5">
                <div className="avatar-xl mx-auto mb-4">
                  <span className="avatar-title text-bg-light rounded-circle fs-1">
                    <i className="ti ti-chart-line text-primary"></i>
                  </span>
                </div>
                <h4 className="mb-3 fw-semibold">Welcome to Market Data Browser</h4>
                <p className="text-muted fs-md">
                  Select a market from the sidebar and choose a data context to get started.
                </p>
              </div>
            </div>
          )}

          {selectedDataContext?.type === 'table' && <TableDisplay />}
          {selectedDataContext?.type === 'chart' && <ChartDisplay />}
        </div>
      </div>
    </MainLayout>
  );
}

export default App;
