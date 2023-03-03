using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class AuditConfiguration
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.Detail)
            .IsRequired();
        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();
    }
}
