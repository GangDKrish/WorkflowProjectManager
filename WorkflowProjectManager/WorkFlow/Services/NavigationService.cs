using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkflowProjectManager.WorkFlow.Services;

public class NavigationService : INavigationService
{
    private object? _currentViewModel;

    public object? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            if (!Equals(_currentViewModel, value))
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }

    public void NavigateTo(object viewModel)
    {
        CurrentViewModel = viewModel;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
