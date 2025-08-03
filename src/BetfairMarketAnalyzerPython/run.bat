@echo off
echo Betfair Market Analyzer Python - Quick Start
echo =============================================

REM Check if Python is available
python --version >nul 2>&1
if errorlevel 1 (
    echo Error: Python is not installed or not in PATH
    echo Please install Python 3.8 or later
    pause
    exit /b 1
)

REM Check if pandas is installed
python -c "import pandas" >nul 2>&1
if errorlevel 1 (
    echo Installing required dependencies...
    python -m pip install -r requirements.txt
    if errorlevel 1 (
        echo Error: Failed to install dependencies
        pause
        exit /b 1
    )
)

echo Starting Betfair Market Analyzer...
echo.
python main.py

pause
