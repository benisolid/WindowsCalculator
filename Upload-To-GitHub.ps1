$ErrorActionPreference = "Stop"

# Run this script from the extracted WindowsCalculator-Starter folder.
if (-not (Test-Path ".\WindowsCalculator\WindowsCalculator\WindowsCalculator.csproj")) {
    throw "Project file not found. Open PowerShell in the extracted WindowsCalculator-Starter folder and run this script again."
}

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    throw "Git is not installed. Install Git for Windows from https://git-scm.com/download/win, then reopen PowerShell."
}

if (-not (Test-Path ".git")) {
    git init
}

git branch -M main
git add .
git commit -m "Initial Windows Calculator project" 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "No new commit was created (possibly because there are no changes). Continuing..."
}

$remote = git remote
if ($remote -contains "origin") {
    git remote set-url origin "https://github.com/benisolid/WindowsCalculator.git"
} else {
    git remote add origin "https://github.com/benisolid/WindowsCalculator.git"
}

git push -u origin main
if ($LASTEXITCODE -ne 0) {
    throw "Push failed. Check GitHub sign-in/permissions and retry: git push -u origin main"
}

Write-Host "Upload completed: https://github.com/benisolid/WindowsCalculator"
