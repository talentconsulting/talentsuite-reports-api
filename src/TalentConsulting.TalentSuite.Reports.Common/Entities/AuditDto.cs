using System.Diagnostics.CodeAnalysis;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record AuditDto
{
    private AuditDto() { }

    public AuditDto(string id, DateTime created, string detail, string userid)
    {
        Id = id;
        Created = created;
        Detail = detail;
        UserId = userid;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public string Detail { get; init; } = default!;
    public string UserId { get; init; } = default!;
}