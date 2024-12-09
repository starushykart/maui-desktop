using System.Diagnostics;
using Hackathon.App.Services.Abstractions;

namespace Hackathon.App;

public class FolderOpener : IFolderOpener
{
    public void OpenFolder(string folderPath)
    {
        if (!string.IsNullOrEmpty(folderPath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = folderPath,
                UseShellExecute = true
            });
        }
    }
}