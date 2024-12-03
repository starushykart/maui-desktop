using CommunityToolkit.Mvvm.Input;
using Hackathon.App.Models;

namespace Hackathon.App.PageModels;

public interface IProjectTaskPageModel
{
    IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
    bool IsBusy { get; }
}