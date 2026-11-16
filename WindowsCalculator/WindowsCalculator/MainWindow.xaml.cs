using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowsCalculator;

public partial class MainWindow : Window
{
    private double _storedValue;
    private string? _pendingOperator;
    private bool _startNewEntry = true;
    private readonly List<string> _history = [];

    private double CurrentValue =>
        double.TryParse(DisplayText.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            ? value : 0;

    public MainWindow() => InitializeComponent();

    private void SetDisplay(double value)
    {
        if (!double.IsFinite(value))
        {
            DisplayText.Text = "Error";
            _startNewEntry = true;
            return;
        }

        DisplayText.Text = value.ToString("G12", CultureInfo.InvariantCulture);
    }

    private void Digit_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        var digit = button.Content?.ToString() ?? "0";
        if (_startNewEntry || DisplayText.Text == "0" || DisplayText.Text == "Error")
            DisplayText.Text = digit;
        else if (DisplayText.Text.Length < 16)
            DisplayText.Text += digit;
        _startNewEntry = false;
    }

    private void Decimal_Click(object sender, RoutedEventArgs e)
    {
        if (_startNewEntry) { DisplayText.Text = "0."; _startNewEntry = false; }
        else if (!DisplayText.Text.Contains('.')) DisplayText.Text += ".";
    }

    private void Operator_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (_pendingOperator is not null && !_startNewEntry) Calculate();
        _storedValue = CurrentValue;
        _pendingOperator = button.Content?.ToString();
        ExpressionText.Text = $"{DisplayText.Text} {_pendingOperator}";
        _startNewEntry = true;
    }

    private void Equals_Click(object sender, RoutedEventArgs e)
    {
        if (_pendingOperator is null) return;
        var expression = $"{_storedValue} {_pendingOperator} {CurrentValue}";
        Calculate();
        _history.Insert(0, $"{expression} = {DisplayText.Text}");
        ExpressionText.Text = expression + " =";
        _pendingOperator = null;
        _startNewEntry = true;
    }

    private void Calculate()
    {
        var right = CurrentValue;
        double result = _pendingOperator switch
        {
            "+" => _storedValue + right,
            "−" => _storedValue - right,
            "×" => _storedValue * right,
            "÷" => right == 0 ? double.NaN : _storedValue / right,
            _ => right
        };
        SetDisplay(result);
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        DisplayText.Text = "0";
        ExpressionText.Text = "";
        _storedValue = 0;
        _pendingOperator = null;
        _startNewEntry = true;
    }

    private void ClearEntry_Click(object sender, RoutedEventArgs e)
    {
        DisplayText.Text = "0";
        _startNewEntry = true;
    }

    private void Sign_Click(object sender, RoutedEventArgs e) => SetDisplay(-CurrentValue);

    private void Percent_Click(object sender, RoutedEventArgs e) =>
        SetDisplay(_pendingOperator is null ? CurrentValue / 100 : _storedValue * CurrentValue / 100);

    private void Sqrt_Click(object sender, RoutedEventArgs e) =>
        SetDisplay(CurrentValue < 0 ? double.NaN : Math.Sqrt(CurrentValue));

    private void Square_Click(object sender, RoutedEventArgs e) => SetDisplay(CurrentValue * CurrentValue);

    private void Backspace_Click(object sender, RoutedEventArgs e)
    {
        if (_startNewEntry || DisplayText.Text.Length <= 1) DisplayText.Text = "0";
        else DisplayText.Text = DisplayText.Text[..^1];
        _startNewEntry = false;
    }

    private void History_Click(object sender, RoutedEventArgs e)
    {
        var message = _history.Count == 0 ? "No calculations yet." : string.Join(Environment.NewLine, _history.Take(20));
        MessageBox.Show(this, message, "History", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key >= Key.D0 && e.Key <= Key.D9)
        {
            AddDigit((int)e.Key - (int)Key.D0);
            e.Handled = true;
        }
        else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
        {
            AddDigit((int)e.Key - (int)Key.NumPad0);
            e.Handled = true;
        }
        else if (e.Key == Key.Enter || e.Key == Key.Return) { Equals_Click(this, new RoutedEventArgs()); e.Handled = true; }
        else if (e.Key == Key.Back) { Backspace_Click(this, new RoutedEventArgs()); e.Handled = true; }
        else if (e.Key == Key.Escape) { Clear_Click(this, new RoutedEventArgs()); e.Handled = true; }
        else if (e.Key == Key.Add || e.Key == Key.OemPlus) { SetOperator("+"); e.Handled = true; }
        else if (e.Key == Key.Subtract || e.Key == Key.OemMinus) { SetOperator("−"); e.Handled = true; }
        else if (e.Key == Key.Multiply) { SetOperator("×"); e.Handled = true; }
        else if (e.Key == Key.Divide || e.Key == Key.Oem2) { SetOperator("÷"); e.Handled = true; }
        else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod) { Decimal_Click(this, new RoutedEventArgs()); e.Handled = true; }
    }

    private void AddDigit(int digit)
    {
        if (_startNewEntry || DisplayText.Text == "0" || DisplayText.Text == "Error")
            DisplayText.Text = digit.ToString(CultureInfo.InvariantCulture);
        else if (DisplayText.Text.Length < 16) DisplayText.Text += digit.ToString(CultureInfo.InvariantCulture);
        _startNewEntry = false;
    }

    private void SetOperator(string op)
    {
        if (_pendingOperator is not null && !_startNewEntry) Calculate();
        _storedValue = CurrentValue;
        _pendingOperator = op;
        ExpressionText.Text = $"{DisplayText.Text} {op}";
        _startNewEntry = true;
    }
}
