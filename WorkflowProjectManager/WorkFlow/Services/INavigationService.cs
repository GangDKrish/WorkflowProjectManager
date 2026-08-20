using System.ComponentModel;

namespace WorkflowProjectManager.WorkFlow.Services;

public interface INavigationService : INotifyPropertyChanged
{
    object? CurrentViewModel { get; }

    void NavigateTo(object viewModel);
}
