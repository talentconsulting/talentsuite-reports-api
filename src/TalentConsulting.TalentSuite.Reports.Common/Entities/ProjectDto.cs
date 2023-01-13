namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record ProjectDto
{
    private ProjectDto() { }

    public ProjectDto(string id, string clId, string name, string reference, DateTime startDate, DateTime endDate)
    {
        Id = id;
        ClId = clId;
        Name = name;
        Reference = reference;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Id { get; init; } = default!;
    public string ClId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Reference { get; init; } = default!;
    public DateTime StartDate { get; init; } = default!;
    public DateTime EndDate { get; init; } = default!;
}

