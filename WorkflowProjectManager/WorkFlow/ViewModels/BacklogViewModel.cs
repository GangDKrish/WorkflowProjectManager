using System.Collections.ObjectModel;
using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager.WorkFlow.ViewModels;

public class BacklogViewModel : ViewModelBase
{
    private readonly IWorkItemService _workItemService;

    public BacklogViewModel(WorkFlowDbContext context)
    {
        _workItemService = new WorkItemService(context);
        BacklogItems = new ObservableCollection<WorkItem>();

        _ = LoadAsync();
    }

    public ObservableCollection<WorkItem> BacklogItems { get; }

    public void Refresh() => _ = LoadAsync();

    private async System.Threading.Tasks.Task LoadAsync()
    {
        var items = await _workItemService.GetBacklogWorkItemsAsync();
        BacklogItems.Clear();
        foreach (var item in items)
        {
            BacklogItems.Add(item);
        }
    }
}
