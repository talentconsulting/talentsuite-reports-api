using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.ClId)
            .IsRequired();
        builder.Property(t => t.Name)
            .IsRequired();
        builder.Property(t => t.Reference)
            .IsRequired();
        builder.Property(t => t.StartDate)
            .IsRequired();
        builder.Property(t => t.EndDate)
            .IsRequired();
        builder.Property(t => t.Created)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();
    }
}
