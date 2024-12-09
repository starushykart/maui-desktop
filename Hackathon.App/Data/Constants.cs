namespace Hackathon.App.Data;

public static class Constants
{
    public const string DatabaseFilename = "AppSQLite3.db3";

    public static string DatabasePath =>
        $"Data Source={Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename)}";
}