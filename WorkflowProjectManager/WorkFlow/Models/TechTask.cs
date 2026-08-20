using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Models;

public class TechTask : WorkItem
{
    public int EstimatedHours { get; set; }

    public TechTask()
    {
        Type = WorkItemType.TechTask;
    }
}
