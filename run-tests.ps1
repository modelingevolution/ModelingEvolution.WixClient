#!/usr/bin/env pwsh

param(
    [string]$ApiKey = $env:Wix__ApiKey,
    [string]$AccountId = $env:Wix__AccountId
)

Write-Host "Running WixClient Integration Tests..." -ForegroundColor Green

# Set environment variables if provided
if ($ApiKey) {
    $env:Wix__ApiKey = $ApiKey
    Write-Host "Using provided API Key" -ForegroundColor Yellow
}

if ($AccountId) {
    $env:Wix__AccountId = $AccountId
    Write-Host "Using provided Account ID" -ForegroundColor Yellow
}

# Run tests
Write-Host "`nExecuting tests..." -ForegroundColor Cyan
dotnet test --logger "console;verbosity=normal" --filter "Category=Integration"

if ($LASTEXITCODE -ne 0) {
    Write-Host "`nTests failed!" -ForegroundColor Red
    exit 1
}

Write-Host "`nAll tests passed!" -ForegroundColor Green