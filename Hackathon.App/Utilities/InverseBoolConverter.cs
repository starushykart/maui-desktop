using System.Globalization;

namespace Hackathon.App.Utilities;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => !(bool)value!;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => !(bool)value!;
}