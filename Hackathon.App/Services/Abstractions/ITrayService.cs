namespace Hackathon.App.Services.Abstractions;

public interface ITrayService
{
    void Initialize();

    Action? ClickHandler { get; set; }
}
