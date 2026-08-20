using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Configurations;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(d => d.Email)
            .IsUnique();

        builder.Property(d => d.Role)
            .HasMaxLength(50);
    }
}
