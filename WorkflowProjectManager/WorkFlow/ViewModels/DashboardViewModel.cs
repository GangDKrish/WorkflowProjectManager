using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Models.Enums;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    private readonly IWorkItemService _workItemService;
    private readonly IProjectService _projectService;
    private readonly ISprintService _sprintService;

    private int _totalWorkItems;
    private int _inProgressWorkItems;
    private int _completedWorkItems;
    private int _activeSprints;

    public DashboardViewModel(WorkFlowDbContext context)
    {
        _workItemService = new WorkItemService(context);
        _projectService = new ProjectService(context);
        _sprintService = new SprintService(context);

        RecentWorkItems = new ObservableCollection<WorkItem>();
        Projects = new ObservableCollection<Project>();

        RefreshCommand = new AsyncRelayCommand(LoadAsync);

        _ = LoadAsync();
    }

    public ICommand RefreshCommand { get; }

    public int TotalWorkItems
    {
        get => _totalWorkItems;
        set => SetProperty(ref _totalWorkItems, value);
    }

    public int InProgressWorkItems
    {
        get => _inProgressWorkItems;
        set => SetProperty(ref _inProgressWorkItems, value);
    }

    public int CompletedWorkItems
    {
        get => _completedWorkItems;
        set => SetProperty(ref _completedWorkItems, value);
    }

    public int ActiveSprints
    {
        get => _activeSprints;
        set => SetProperty(ref _activeSprints, value);
    }

    public ObservableCollection<WorkItem> RecentWorkItems { get; }

    public ObservableCollection<Project> Projects { get; }

    private async Task LoadAsync()
    {
        var workItems = (await _workItemService.GetAllWorkItemsAsync()).ToList();
        var projects = (await _projectService.GetAllProjectsAsync()).ToList();
        var sprints = (await _sprintService.GetAllSprintsAsync()).ToList();

        TotalWorkItems = workItems.Count;
        InProgressWorkItems = workItems.Count(w => w.Status == WorkItemStatus.InProgress || w.Status == WorkItemStatus.InReview);
        CompletedWorkItems = workItems.Count(w => w.Status == WorkItemStatus.Done);
        ActiveSprints = sprints.Count(s => s.StartDate <= System.DateTime.Today && s.EndDate >= System.DateTime.Today);

        RecentWorkItems.Clear();
        foreach (var item in workItems.OrderByDescending(w => w.CreatedAt).Take(5))
        {
            RecentWorkItems.Add(item);
        }

        Projects.Clear();
        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }
}
