# Windows Calculator

A modern dark-themed desktop calculator built with C# and WPF for Windows 10/11.

## Features

- Addition, subtraction, multiplication, and division
- Percentage, sign toggle, square root, and square
- Calculation history
- Keyboard input
- Responsive WPF layout

## Requirements

- Windows 10 or Windows 11
- .NET 8 SDK

## Run

```powershell
dotnet restore
dotnet run --project .\WindowsCalculator\WindowsCalculator.csproj
```

## Publish a standalone Windows build

```powershell
dotnet publish .\WindowsCalculator\WindowsCalculator.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

The published files are placed under `WindowsCalculator\bin\Release\net8.0-windows\win-x64\publish`.
