using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class UserGroupConfiguration
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.Property(t => t.id)
            .IsRequired();
        builder.Property(t => t.Name)
            .IsRequired();
        builder.Property(t => t.ReceivesReports)
            .IsRequired();

        builder.Property(t => t.created)
            .IsRequired();


        builder.HasMany(s => s.Users)
            .WithOne()
            .HasForeignKey(lc => lc.UserGroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
