using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class UserConfiguration
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.Firstname)
            .IsRequired();
        builder.Property(t => t.Lastname)
            .IsRequired();
        builder.Property(t => t.Email)
            .IsRequired();
        builder.Property(t => t.UserGroupId)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();

        builder.HasMany(s => s.Reports)
            .WithOne()
            .HasForeignKey(lc => lc.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
