import React from 'react';
import { Sidebar } from './Sidebar';

interface MainLayoutProps {
  sidebar: React.ReactNode;
  children: React.ReactNode;
}

export const MainLayout: React.FC<MainLayoutProps> = ({ sidebar, children }) => {
  return (
    <div className="wrapper">
      {/* Sidebar */}
      <Sidebar>{sidebar}</Sidebar>

      {/* Main Content Area */}
      <div className="content-page">
        {/* Page Content */}
        <div className="content">
          <div className="container-fluid">
            {children}
          </div>
        </div>

        {/* Footer */}
        <footer className="footer">
          <div className="container-fluid">
            <div className="row">
              <div className="col-12 text-center">
                {new Date().getFullYear()} © Betfair Market Data Browser
              </div>
            </div>
          </div>
        </footer>
      </div>
    </div>
  );
};
