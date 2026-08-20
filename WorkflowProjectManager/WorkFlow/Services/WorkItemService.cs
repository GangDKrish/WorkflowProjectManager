using WorkflowProjectManager.WorkFlow.Data;
using WorkflowProjectManager.WorkFlow.Data.Repositories;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Services;

public class WorkItemService : IWorkItemService
{
    private readonly WorkItemRepository _repository;

    public WorkItemService(WorkFlowDbContext context)
    {
        _repository = new WorkItemRepository(context);
    }

    public async Task<IEnumerable<WorkItem>> GetAllWorkItemsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetBacklogWorkItemsAsync()
    {
        return await _repository.GetBacklogAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetWorkItemsBySprintAsync(int sprintId)
    {
        return await _repository.GetBySprintIdAsync(sprintId);
    }

    public async Task<WorkItem> CreateWorkItemAsync(WorkItem workItem)
    {
        return await _repository.AddAsync(workItem);
    }

    public async Task<bool> UpdateStatusAsync(int workItemId, WorkItemStatus status)
    {
        var workItem = await _repository.GetByIdAsync(workItemId);
        if (workItem == null) return false;

        workItem.Status = status;
        await _repository.UpdateAsync(workItem);
        return true;
    }
}

