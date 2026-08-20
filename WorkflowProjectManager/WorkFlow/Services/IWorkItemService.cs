using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowProjectManager.WorkFlow.Models;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Services;

public interface IWorkItemService
{
    Task<IEnumerable<WorkItem>> GetAllWorkItemsAsync();

    Task<IEnumerable<WorkItem>> GetBacklogWorkItemsAsync();

    Task<IEnumerable<WorkItem>> GetWorkItemsBySprintAsync(int sprintId);

    Task<WorkItem> CreateWorkItemAsync(WorkItem workItem);

    Task<bool> UpdateStatusAsync(int workItemId, WorkItemStatus status);
}
