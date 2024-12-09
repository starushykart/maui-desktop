namespace Hackathon.App.Utilities;

/// <summary>
/// Task Utilities.
/// </summary>
public static class TaskUtilities
{
    /// <summary>
    /// Fire and Forget Safe Async.
    /// </summary>
    /// <param name="task">Task to Fire and Forget.</param>
    /// <param name="handler">Error Handler.</param>
    public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler? handler = null)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            handler?.HandleError(ex);
        }
    }
    
    public static async Task OnUIThreadAsync(Action act)
    {
        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher != null)
            await dispatcher.DispatchAsync(act);
    }
}