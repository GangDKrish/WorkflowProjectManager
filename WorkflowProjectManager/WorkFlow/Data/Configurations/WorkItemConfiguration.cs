using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(w => w.Description)
            .HasMaxLength(2000);

        builder.Property(w => w.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(w => w.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(w => w.Priority)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(w => w.StoryPoints)
            .IsRequired();

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.Property(w => w.AssignedDeveloperId);

        builder.HasMany(w => w.Comments)
            .WithOne()
            .HasForeignKey(c => c.WorkItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
