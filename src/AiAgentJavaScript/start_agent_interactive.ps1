#!/usr/bin/env pwsh
# Start the interactive AI agent

Write-Host "🚀 Starting Bfexplorer AI Agent..." -ForegroundColor Green

# Check if GitHub token is set
if (-not $env:GITHUB_TOKEN) {
    Write-Host "❌ Error: GITHUB_TOKEN environment variable is not set." -ForegroundColor Red
    Write-Host "Please set your GitHub token first:" -ForegroundColor Yellow
    Write-Host '  $env:GITHUB_TOKEN = "your-token-here"' -ForegroundColor Cyan
    exit 1
}

# Check if node_modules exists
if (-not (Test-Path "node_modules")) {
    Write-Host "📦 Installing dependencies..." -ForegroundColor Yellow
    npm install
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Failed to install dependencies" -ForegroundColor Red
        exit 1
    }
}

Write-Host "✅ Starting interactive agent..." -ForegroundColor Green
node ./dist/agent_interactive.js
