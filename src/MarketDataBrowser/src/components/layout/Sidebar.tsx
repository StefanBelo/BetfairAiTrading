import React from 'react';
import { useStore } from '../../store/useStore';

interface SidebarProps {
  children?: React.ReactNode;
}

export const Sidebar: React.FC<SidebarProps> = ({ children }) => {
  return (
    <div className="sidenav-menu">
      {/* Brand Logo */}
      <a href="#" className="logo">
        <span className="logo logo-light">
          <span className="logo-lg">
            <span className="fs-4 fw-bold text-primary">📊 Market Browser</span>
          </span>
          <span className="logo-sm">
            <span className="fs-4">📊</span>
          </span>
        </span>

        <span className="logo logo-dark">
          <span className="logo-lg">
            <span className="fs-4 fw-bold text-primary">📊 Market Browser</span>
          </span>
          <span className="logo-sm">
            <span className="fs-4">📊</span>
          </span>
        </span>
      </a>

      {/* Sidebar Hover Menu Toggle Button */}
      <button className="button-on-hover">
        <i className="ti ti-menu-4 fs-22 align-middle"></i>
      </button>

      {/* Full Sidebar Menu Close Button */}
      <button className="button-close-offcanvas">
        <i className="ti ti-x align-middle"></i>
      </button>

      <div className="scrollbar" data-simplebar>
        {/* Sidebar Content */}
        <div className="sidenav-content">
          {children}
        </div>
      </div>
    </div>
  );
};
