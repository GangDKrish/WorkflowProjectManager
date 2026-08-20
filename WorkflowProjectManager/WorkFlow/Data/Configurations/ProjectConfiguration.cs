using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowProjectManager.WorkFlow.Models;

namespace WorkflowProjectManager.WorkFlow.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.HasMany(p => p.Sprints)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Developers)
            .WithMany()
            .UsingEntity(j => j.ToTable("ProjectDevelopers"));
    }
}
