using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Models;

public class UserStory : WorkItem
{
    public string AcceptanceCriteria { get; set; } = string.Empty;

    public UserStory()
    {
        Type = WorkItemType.UserStory;
    }
}
