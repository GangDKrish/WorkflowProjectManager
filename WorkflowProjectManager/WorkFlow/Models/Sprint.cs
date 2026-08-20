using System;
using System.Collections.ObjectModel;

namespace WorkflowProjectManager.WorkFlow.Models;

public class Sprint
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int GoalPoints { get; set; }

    public ObservableCollection<WorkItem> WorkItems { get; set; } = new();
}
