using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IThemeService _themeService;
    private string _applicationName;
    private string _version;
    private bool _darkModeEnabled;
    private bool _notificationsEnabled;

    public SettingsViewModel()
    {
        _themeService = new ThemeService();

        ApplicationName = "Developer Sprint Monitor";
        Version = "1.0.0";
        NotificationsEnabled = true;

        // Load saved theme preference
        var currentTheme = _themeService.GetCurrentTheme();
        DarkModeEnabled = currentTheme.Equals("Dark", System.StringComparison.OrdinalIgnoreCase);

        SaveSettingsCommand = new RelayCommand(SaveSettings);
        ResetSettingsCommand = new RelayCommand(ResetSettings);
    }

    public string ApplicationName
    {
        get => _applicationName;
        set => SetProperty(ref _applicationName, value);
    }

    public string Version
    {
        get => _version;
        set => SetProperty(ref _version, value);
    }

    public bool DarkModeEnabled
    {
        get => _darkModeEnabled;
        set
        {
            if (SetProperty(ref _darkModeEnabled, value))
            {
                // Apply theme change immediately
                _themeService.ApplyTheme(value ? "Dark" : "Light");
            }
        }
    }

    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        set => SetProperty(ref _notificationsEnabled, value);
    }

    public ICommand SaveSettingsCommand { get; }

    public ICommand ResetSettingsCommand { get; }

    private void SaveSettings()
    {
        // Settings are auto-saved when changed
        System.Windows.MessageBox.Show("Settings saved successfully!", "Success", 
            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
    }

    private void ResetSettings()
    {
        DarkModeEnabled = false;
        NotificationsEnabled = true;
    }
}
