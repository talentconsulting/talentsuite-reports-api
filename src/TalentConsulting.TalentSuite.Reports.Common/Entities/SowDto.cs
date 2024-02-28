using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record SowDto
{
    private SowDto() { }

    public SowDto(string id, DateTime created, ICollection<SowFileDto> files, bool ischangerequest, DateTime startdate, DateTime enddate, string projectId)
    {
        Id = id;
        Created = created;
        Files = files;
        IsChangeRequest = ischangerequest;
        StartDate = startdate;
        EndDate = enddate;
        ProjectId = projectId;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public byte[] File { get; init; } = default!;
    public bool IsChangeRequest { get; init; } = default!;
    public DateTime StartDate { get; init; } = default!;
    public DateTime EndDate { get; init; } = default!;
    public string ProjectId { get; init; } = default!;

#if ADD_ENTITY_NAV
    public ProjectDto Project { get; set; } = default!;
#endif

    public virtual ICollection<SowFileDto> Files { get; set; } = new Collection<SowFileDto>();

}
