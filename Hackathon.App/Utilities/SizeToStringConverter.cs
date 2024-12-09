namespace Hackathon.App.Utilities;

public class SizeToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not long size)
            return string.Empty;

        if (size < 1024)
            return $"{size:0.##} B";

        string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
        var order = 0;

        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }

        return $"{size:0.##} {sizes[order]}";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => string.Empty;
}