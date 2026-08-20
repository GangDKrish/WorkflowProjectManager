using System;
using System.Collections.ObjectModel;

namespace WorkflowProjectManager.WorkFlow.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public ObservableCollection<Sprint> Sprints { get; set; } = new();

    public ObservableCollection<Developer> Developers { get; set; } = new();
}

