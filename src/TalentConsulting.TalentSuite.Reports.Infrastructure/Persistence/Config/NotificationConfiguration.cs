using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class NotificationConfiguration
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(t => t.id)
            .IsRequired();
        builder.Property(t => t.NextRetryDate)
            .IsRequired();
        builder.Property(t => t.Content)
            .IsRequired();
        builder.Property(t => t.Title)
            .IsRequired();
        builder.Property(t => t.Status)
            .IsRequired();

        builder.Property(t => t.created)
            .IsRequired();


        builder.HasMany(s => s.Recipients)
            .WithOne()
            .HasForeignKey(lc => lc.Notificationid)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
