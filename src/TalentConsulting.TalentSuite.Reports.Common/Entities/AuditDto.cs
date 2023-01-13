﻿namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record AuditDto
{
    private AuditDto() { }

    public AuditDto(string id, DateTime created, string detail, string userid)
    {
        Id = id;
        CreatedDate = created;
        Detail = detail;
        UserId = userid;
    }

    public string Id { get; init; } = default!;
    public DateTime CreatedDate { get; init; } = default!;
    public string Detail { get; init; } = default!;
    public string UserId { get; init; } = default!;
}