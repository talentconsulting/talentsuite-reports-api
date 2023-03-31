using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class SowConfiguration
{
    public void Configure(EntityTypeBuilder<Sow> builder)
    {
        builder.Property(t => t.id)
            .IsRequired();
        builder.Property(t => t.File)
            .IsRequired();
        builder.Property(t => t.IsChangeRequest)
            .IsRequired();
        builder.Property(t => t.SowStartDate)
            .IsRequired();
        builder.Property(t => t.SowEndDate)
            .IsRequired();
        builder.Property(t => t.ProjectId)
            .IsRequired();
        builder.Property(t => t.created)
            .IsRequired();
    }
}
