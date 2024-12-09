namespace Hackathon.App.Services.Abstractions;

public interface INotificationService
{
    void ShowNotification(string title, string body);
}