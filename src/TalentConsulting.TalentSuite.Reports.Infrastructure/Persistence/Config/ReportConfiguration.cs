using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class ReportConfiguration
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(t => t.id)
            .IsRequired();
        builder.Property(t => t.PlannedTasks)
            .IsRequired();
        builder.Property(t => t.CompletedTasks)
            .IsRequired();
        builder.Property(t => t.Weeknumber)
            .IsRequired();
        builder.Property(t => t.SubmissionDate)
            .IsRequired();
        builder.Property(t => t.ProjectId)
            .IsRequired();
        builder.Property(t => t.created)
            .IsRequired();


        builder.HasMany(s => s.Risks)
            .WithOne()
            .HasForeignKey(lc => lc.ReportId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
