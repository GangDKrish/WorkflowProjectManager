namespace WorkflowProjectManager.WorkFlow.Models;

public class Developer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public List<WorkItem> AssignedWorkItems { get; set; }
}
