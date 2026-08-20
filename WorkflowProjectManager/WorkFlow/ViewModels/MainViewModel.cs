using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Data;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class MainViewModel : ViewModelBase
{
    private object? _currentView;
    private string _title;
    private readonly WorkFlowDbContext _context;

    public MainViewModel()
    {
        Title = "Developer Sprint Monitor";
        _context = new WorkFlowDbContext();

        ShowDashboardCommand = new RelayCommand(ShowDashboard);
        ShowBacklogCommand = new RelayCommand(ShowBacklog);
        ShowSprintPlanningCommand = new RelayCommand(ShowSprintPlanning);
        ShowWorkItemsCommand = new RelayCommand(ShowWorkItems);
        ShowProjectsCommand = new RelayCommand(ShowProjects);
        ShowSettingsCommand = new RelayCommand(ShowSettings);

        DashboardViewModel = new DashboardViewModel(_context);
        BacklogViewModel = new BacklogViewModel(_context);
        SprintPlanningViewModel = new SprintPlanningViewModel(_context);
        WorkItemViewModel = new WorkItemViewModel(_context);
        ProjectViewModel = new ProjectViewModel(_context);
        SettingsViewModel = new SettingsViewModel();

        CurrentView = DashboardViewModel;
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public object? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public DashboardViewModel DashboardViewModel { get; }
    public BacklogViewModel BacklogViewModel { get; }
    public SprintPlanningViewModel SprintPlanningViewModel { get; }
    public WorkItemViewModel WorkItemViewModel { get; }
    public ProjectViewModel ProjectViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }

    public ICommand ShowDashboardCommand { get; }
    public ICommand ShowBacklogCommand { get; }
    public ICommand ShowSprintPlanningCommand { get; }
    public ICommand ShowWorkItemsCommand { get; }
    public ICommand ShowProjectsCommand { get; }
    public ICommand ShowSettingsCommand { get; }

    private void ShowDashboard() => CurrentView = DashboardViewModel;

    private void ShowBacklog()
    {
        BacklogViewModel.Refresh();
        CurrentView = BacklogViewModel;
    }

    private void ShowSprintPlanning() => CurrentView = SprintPlanningViewModel;

    private void ShowWorkItems() => CurrentView = WorkItemViewModel;

    private void ShowProjects() => CurrentView = ProjectViewModel;

    private void ShowSettings() => CurrentView = SettingsViewModel;
}
