import React from 'react';

export const Topbar: React.FC = () => {
  return (
    <header className="app-topbar">
      <div className="container-fluid topbar-menu">
        <div className="d-flex align-items-center gap-2">
          {/* Sidebar Menu Toggle Button */}
          <button className="sidenav-toggle-button btn btn-primary btn-icon">
            <i className="ti ti-menu-4 fs-22"></i>
          </button>

          {/* Search */}
          <div className="app-search d-none d-xl-flex">
            <input
              type="search"
              className="form-control topbar-search"
              name="search"
              placeholder="Search markets..."
            />
            <i className="ti ti-search app-search-icon text-muted"></i>
          </div>

          {/* Title */}
          <div className="ms-2 d-none d-md-block">
            <h5 className="mb-0 text-muted fw-normal">Betfair Market Analysis</h5>
          </div>
        </div>

        <div className="d-flex align-items-center gap-2">
          {/* Theme Toggle */}
          <div className="topbar-item">
            <button
              className="topbar-link"
              type="button"
              id="light-dark-mode"
              data-bs-toggle="tooltip"
              data-bs-placement="left"
              title="Toggle theme"
            >
              <i className="ti ti-moon fs-xxl"></i>
            </button>
          </div>

          {/* Notifications */}
          <div className="topbar-item">
            <button className="topbar-link" type="button">
              <i className="ti ti-bell fs-xxl"></i>
              <span className="badge text-bg-danger badge-circle topbar-badge">3</span>
            </button>
          </div>

          {/* Settings */}
          <div className="topbar-item">
            <button className="topbar-link" type="button">
              <i className="ti ti-settings fs-xxl"></i>
            </button>
          </div>

          {/* User Profile */}
          <div className="topbar-item">
            <div className="dropdown">
              <button
                className="topbar-link dropdown-toggle drop-arrow-none p-0"
                data-bs-toggle="dropdown"
                data-bs-offset="0,22"
                type="button"
                aria-haspopup="false"
                aria-expanded="false"
              >
                <img
                  src="/inspinia-assets/images/users/user-2.jpg"
                  alt="user"
                  className="rounded-circle avatar-sm"
                />
              </button>
              <div className="dropdown-menu dropdown-menu-end">
                <div className="dropdown-header">
                  <h6 className="text-overflow m-0">Welcome!</h6>
                </div>
                <a href="#" className="dropdown-item">
                  <i className="ti ti-user-circle me-2 fs-17"></i>
                  <span>Profile</span>
                </a>
                <a href="#" className="dropdown-item">
                  <i className="ti ti-settings-2 me-2 fs-17"></i>
                  <span>Settings</span>
                </a>
                <div className="dropdown-divider"></div>
                <a href="#" className="dropdown-item text-danger fw-semibold">
                  <i className="ti ti-logout-2 me-2 fs-17"></i>
                  <span>Log Out</span>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
};
