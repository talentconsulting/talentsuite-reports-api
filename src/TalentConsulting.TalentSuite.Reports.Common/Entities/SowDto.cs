using System.Reflection.Metadata;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record SowDto
{
    private SowDto() { }

    public SowDto(string id, DateTime created, string blob, bool ischangerequest, DateTime startdate, DateTime enddate, int projectid)
    {
        Id = id;
        Created = created;
        Blob = blob;
        IsChangeRequest = ischangerequest;
        StartDate = startdate;
        EndDate = enddate;
        ProjectId = projectid;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public string Blob { get; init; } = default!;
    public bool IsChangeRequest { get; init; } = default!;
    public DateTime StartDate { get; init; } = default!;
    public DateTime EndDate { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
}
