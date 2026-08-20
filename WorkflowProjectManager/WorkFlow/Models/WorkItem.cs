using System;
using System.Collections.ObjectModel;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Models;

public class WorkItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public WorkItemType Type { get; set; }

    public WorkItemStatus Status { get; set; } = WorkItemStatus.Backlog;

    public Priority Priority { get; set; } = Priority.Medium;

    public int StoryPoints { get; set; }

    public int SprintId { get; set; }

    public int ProjectId { get; set; }

    public int? AssignedDeveloperId { get; set; }

    public Developer? AssignedDeveloper { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ObservableCollection<WorkItemComment> Comments { get; set; } = new();
}
