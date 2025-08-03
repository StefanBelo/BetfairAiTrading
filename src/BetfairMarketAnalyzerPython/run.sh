#!/bin/bash

echo "Betfair Market Analyzer Python - Quick Start"
echo "============================================="

# Check if Python is available
if ! command -v python3 &> /dev/null && ! command -v python &> /dev/null; then
    echo "Error: Python is not installed or not in PATH"
    echo "Please install Python 3.8 or later"
    exit 1
fi

# Use python3 if available, otherwise python
PYTHON_CMD="python3"
if ! command -v python3 &> /dev/null; then
    PYTHON_CMD="python"
fi

# Check if pandas is installed
$PYTHON_CMD -c "import pandas" &> /dev/null
if [ $? -ne 0 ]; then
    echo "Installing required dependencies..."
    $PYTHON_CMD -m pip install -r requirements.txt
    if [ $? -ne 0 ]; then
        echo "Error: Failed to install dependencies"
        exit 1
    fi
fi

echo "Starting Betfair Market Analyzer..."
echo
$PYTHON_CMD main.py
