using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

[ExcludeFromCodeCoverage]
public static class RiskConfiguration
{
    public static void Configure(EntityTypeBuilder<Risk> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.ReportId)
            .IsRequired();
        builder.Property(t => t.RiskDetails)
            .IsRequired();
        builder.Property(t => t.RiskMitigation)
            .IsRequired();
        builder.Property(t => t.RagStatus)
            .IsRequired();
    }
}
