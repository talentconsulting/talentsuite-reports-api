using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class ClientProjectConfiguration
{
    public void Configure(EntityTypeBuilder<ClientProject> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.ClientId)
            .IsRequired();
        builder.Property(t => t.ProjectId)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();

    }
}
