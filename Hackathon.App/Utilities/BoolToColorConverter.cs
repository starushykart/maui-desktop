namespace Hackathon.App.Utilities;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool isOnline)
        {
            return isOnline ? Colors.Green : Colors.Red;
        }
        
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => string.Empty;
}