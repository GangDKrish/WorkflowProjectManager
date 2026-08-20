using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WorkflowProjectManager.WorkFlow.Commands;
using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Models.Enums;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class WorkItemViewModel : ViewModelBase
{
    private readonly IWorkItemService _workItemService;
    private WorkItem? _selectedWorkItem;
    private string _newTitle = string.Empty;
    private string _newDescription = string.Empty;
    private WorkItemType _newType = WorkItemType.UserStory;
    private Priority _newPriority = Priority.Medium;
    private int _newStoryPoints = 1;

    public WorkItemViewModel(WorkFlowDbContext context)
    {
        _workItemService = new WorkItemService(context);
        WorkItems = new ObservableCollection<WorkItem>();

        RefreshCommand = new AsyncRelayCommand(LoadAsync);
        AddCommand = new AsyncRelayCommand(AddWorkItemAsync);
        DeleteCommand = new RelayCommand(DeleteWorkItem, CanDeleteWorkItem);

        _ = LoadAsync();
    }

    public ObservableCollection<WorkItem> WorkItems { get; }

    public WorkItem? SelectedWorkItem
    {
        get => _selectedWorkItem;
        set
        {
            if (SetProperty(ref _selectedWorkItem, value))
            {
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string NewTitle
    {
        get => _newTitle;
        set => SetProperty(ref _newTitle, value);
    }

    public string NewDescription
    {
        get => _newDescription;
        set => SetProperty(ref _newDescription, value);
    }

    public WorkItemType NewType
    {
        get => _newType;
        set => SetProperty(ref _newType, value);
    }

    public Priority NewPriority
    {
        get => _newPriority;
        set => SetProperty(ref _newPriority, value);
    }

    public int NewStoryPoints
    {
        get => _newStoryPoints;
        set => SetProperty(ref _newStoryPoints, value);
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    private async System.Threading.Tasks.Task LoadAsync()
    {
        var items = await _workItemService.GetAllWorkItemsAsync();
        WorkItems.Clear();
        foreach (var item in items)
        {
            WorkItems.Add(item);
        }
    }

    private async System.Threading.Tasks.Task AddWorkItemAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTitle))
        {
            MessageBox.Show("Title is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var newItem = new WorkItem
        {
            Title = NewTitle,
            Description = NewDescription,
            Type = NewType,
            Priority = NewPriority,
            StoryPoints = NewStoryPoints,
            Status = WorkItemStatus.Backlog,
            ProjectId = 1,
            SprintId = 1,
            CreatedAt = System.DateTime.UtcNow
        };

        await _workItemService.CreateWorkItemAsync(newItem);

        // Clear form
        NewTitle = string.Empty;
        NewDescription = string.Empty;
        NewType = WorkItemType.UserStory;
        NewPriority = Priority.Medium;
        NewStoryPoints = 1;

        await LoadAsync();
        MessageBox.Show("Work item added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private bool CanDeleteWorkItem()
    {
        return SelectedWorkItem != null;
    }

    private async void DeleteWorkItem()
    {
        if (SelectedWorkItem == null) return;

        var result = MessageBox.Show(
            $"Are you sure you want to delete '{SelectedWorkItem.Title}'?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            WorkItems.Remove(SelectedWorkItem);
            MessageBox.Show("Work item deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}