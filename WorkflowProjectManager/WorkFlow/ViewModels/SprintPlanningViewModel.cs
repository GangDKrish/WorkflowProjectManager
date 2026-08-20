using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class SprintPlanningViewModel : ViewModelBase
{
    private readonly ISprintService _sprintService;
    private readonly IWorkItemService _workItemService;
    private Sprint? _selectedSprint;
    private WorkItem? _selectedBacklogItem;
    private string _newSprintName = string.Empty;
    private DateTime _newStartDate = DateTime.Today;
    private DateTime _newEndDate = DateTime.Today.AddDays(14);
    private int _newGoalPoints = 30;

    public SprintPlanningViewModel(WorkFlowDbContext context)
    {
        _sprintService = new SprintService(context);
        _workItemService = new WorkItemService(context);

        Sprints = new ObservableCollection<Sprint>();
        BacklogItems = new ObservableCollection<WorkItem>();

        AddToSprintCommand = new RelayCommand(AddToSprint, CanAddToSprint);
        CreateSprintCommand = new AsyncRelayCommand(CreateSprintAsync);

        _ = LoadAsync();
    }

    public ObservableCollection<Sprint> Sprints { get; }

    public ObservableCollection<WorkItem> BacklogItems { get; }

    public Sprint? SelectedSprint
    {
        get => _selectedSprint;
        set => SetProperty(ref _selectedSprint, value);
    }

    public WorkItem? SelectedBacklogItem
    {
        get => _selectedBacklogItem;
        set
        {
            if (SetProperty(ref _selectedBacklogItem, value))
            {
                (AddToSprintCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
    }

    public ICommand AddToSprintCommand { get; }
    public ICommand CreateSprintCommand { get; }

    public string NewSprintName { get => _newSprintName; set => SetProperty(ref _newSprintName, value); }
    public DateTime NewStartDate { get => _newStartDate; set => SetProperty(ref _newStartDate, value); }
    public DateTime NewEndDate { get => _newEndDate; set => SetProperty(ref _newEndDate, value); }
    public int NewGoalPoints { get => _newGoalPoints; set => SetProperty(ref _newGoalPoints, value); }

    private async System.Threading.Tasks.Task CreateSprintAsync()
    {
        if (string.IsNullOrWhiteSpace(NewSprintName))
        {
            MessageBox.Show("Sprint name is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newSprint = new Sprint
        {
            Name = NewSprintName,
            StartDate = NewStartDate,
            EndDate = NewEndDate,
            GoalPoints = NewGoalPoints
        };

        await _sprintService.CreateSprintAsync(newSprint);
        NewSprintName = string.Empty;
        NewStartDate = DateTime.Today;
        NewEndDate = DateTime.Today.AddDays(14);
        NewGoalPoints = 30;
        await LoadAsync();
        MessageBox.Show("Sprint created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private async System.Threading.Tasks.Task LoadAsync()
    {
        var sprints = await _sprintService.GetAllSprintsAsync();
        var backlog = await _workItemService.GetBacklogWorkItemsAsync();

        Sprints.Clear();
        foreach (var sprint in sprints)
        {
            Sprints.Add(sprint);
        }

        BacklogItems.Clear();
        foreach (var item in backlog)
        {
            BacklogItems.Add(item);
        }

        SelectedSprint = Sprints.Count > 0 ? Sprints[0] : null;
    }

    private bool CanAddToSprint() => SelectedSprint is not null && SelectedBacklogItem is not null;

    private void AddToSprint()
    {
        if (SelectedSprint is null || SelectedBacklogItem is null)
        {
            return;
        }

        SelectedBacklogItem.SprintId = SelectedSprint.Id;
        SelectedSprint.WorkItems.Add(SelectedBacklogItem);
        BacklogItems.Remove(SelectedBacklogItem);
        SelectedBacklogItem = null;
    }
}
