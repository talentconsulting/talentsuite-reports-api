using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace TalentConsulting.TalentSuite.ReportsApi.Db;

#pragma warning disable EF1001

/// <summary>A replacement for <see cref="NpgsqlHistoryRepository"/>
/// to convert the migration table id columns to snake case.

public class SnakeCaseHistoryContext(HistoryRepositoryDependencies dependencies) : NpgsqlHistoryRepository(dependencies)
{
    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        base.ConfigureTable(history);

        history.Property(h => h.MigrationId).HasColumnName("migration_id");
        history.Property(h => h.ProductVersion).HasColumnName("product_version");
    }
}

#pragma warning restore EF1001