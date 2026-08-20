using System.IO;
using Microsoft.EntityFrameworkCore;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data;

public class WorkFlowDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Sprint> Sprints { get; set; } = null!;
    public DbSet<WorkItem> WorkItems { get; set; } = null!;
    public DbSet<Developer> Developers { get; set; } = null!;
    public DbSet<WorkItemComment> WorkItemComments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "workflow.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkFlowDbContext).Assembly);

        // Optional WorkItem -> Developer relationship
        modelBuilder.Entity<WorkItem>()
            .HasOne(w => w.AssignedDeveloper)
            .WithMany(d => d.AssignedWorkItems)
            .HasForeignKey(w => w.AssignedDeveloperId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Developers
        modelBuilder.Entity<Developer>().HasData(
            new Developer { Id = 1, Name = "Alice Johnson", Email = "alice@company.com", Role = "Senior Developer" },
            new Developer { Id = 2, Name = "Bob Smith", Email = "bob@company.com", Role = "Developer II" },
            new Developer { Id = 3, Name = "Charlie Brown", Email = "charlie@company.com", Role = "Developer I" }
        );

        // Seed Projects
        modelBuilder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "Sprint Workflow Manager", Description = "Developer productivity tool", CreatedDate = new DateTime(2024, 1, 1) }
        );

        // Seed Sprints
        modelBuilder.Entity<Sprint>().HasData(
            new Sprint { Id = 1, Name = "Sprint 1", StartDate = new DateTime(2024, 1, 15), EndDate = new DateTime(2024, 1, 29), GoalPoints = 30 }
        );
    }
}

