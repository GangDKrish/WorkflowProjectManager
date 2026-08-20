using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Models;

public class Defect : WorkItem
{
    public string Severity { get; set; } = "Medium";

    public Defect()
    {
        Type = WorkItemType.Defect;
    }
}
