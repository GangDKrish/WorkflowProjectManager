using System;

namespace WorkflowProjectManager.WorkFlow.Models;

public class WorkItemComment
{
    public int Id { get; set; }

    public int WorkItemId { get; set; }

    public int DeveloperId { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
