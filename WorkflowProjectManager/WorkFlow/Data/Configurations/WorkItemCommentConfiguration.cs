using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Configurations;

public class WorkItemCommentConfiguration : IEntityTypeConfiguration<WorkItemComment>
{
    public void Configure(EntityTypeBuilder<WorkItemComment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.DeveloperId)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();
    }
}
