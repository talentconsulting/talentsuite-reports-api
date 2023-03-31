using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class UserProjectRoleConfiguration
{
    public void Configure(EntityTypeBuilder<UserProjectRole> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.UserId)
            .IsRequired();
        builder.Property(t => t.ProjectId)
            .IsRequired();
        builder.Property(t => t.Recievesreports)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();


    }
}
