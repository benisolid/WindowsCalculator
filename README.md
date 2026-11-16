# ماشین حساب ویندوز | Windows Calculator

یک ماشین حساب دسکتاپ با طراحی تیره و مدرن، ساخته‌شده با C# و WPF برای ویندوز ۱۰ و ۱۱.

A modern dark-themed desktop calculator built with C# and WPF for Windows 10 and Windows 11.

## امکانات | Features

- جمع، تفریق، ضرب و تقسیم
- محاسبه درصد
- تغییر علامت مثبت و منفی
- جذر و توان دوم
- تاریخچه محاسبات
- پشتیبانی از صفحه‌کلید
- رابط کاربری واکنش‌گرا

- Addition, subtraction, multiplication, and division
- Percentage and sign toggle
- Square root and square
- Calculation history
- Keyboard input
- Responsive WPF layout

## پیش‌نیازها | Requirements

- ویندوز ۱۰ یا ۱۱
- .NET 8 SDK برای توسعه

- Windows 10 or Windows 11
- .NET 8 SDK for development

## اجرای برنامه | Run

```powershell
dotnet restore .\WindowsCalculator\WindowsCalculator\WindowsCalculator.csproj
dotnet run --project .\WindowsCalculator\WindowsCalculator\WindowsCalculator.csproj
```

## ساخت نسخه مستقل | Publish

```powershell
dotnet publish .\WindowsCalculator\WindowsCalculator\WindowsCalculator.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

## دانلود | Download

[دریافت نسخه‌های برنامه از GitHub Releases](https://github.com/benisolid/WindowsCalculator/releases)

## فناوری‌ها | Technologies

- C#
- .NET 8
- WPF
- XAML