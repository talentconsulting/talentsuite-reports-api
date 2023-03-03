using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Persistence.Config;

public class ClientConfiguration
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.Property(t => t.Name)
            .IsRequired();
        builder.Property(t => t.ContactName)
            .IsRequired();
        builder.Property(t => t.ContactEmail)
            .IsRequired();

        builder.Property(t => t.Created)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasMany(s => s.ClientProjects)
            .WithOne()
            .HasForeignKey(lc => lc.ClientId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
