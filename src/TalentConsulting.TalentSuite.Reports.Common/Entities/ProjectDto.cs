using System.Diagnostics.CodeAnalysis;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record ProjectDto
{
    private ProjectDto() { }

    public ProjectDto(string id, string contactNumber, string name, string reference, DateTime startDate, DateTime endDate, 
        ICollection<ClientProjectDto> clientProjects,
        ICollection<ContactDto> contacts,
        ICollection<ReportDto> reports,
        ICollection<SowDto> sows)
    {
        Id = id;
        ContactNumber = contactNumber;
        Name = name;
        Reference = reference;
        StartDate = startDate;
        EndDate = endDate;
        ClientProjects = clientProjects;
        Contacts = contacts;
        Reports = reports;
        Sows = sows;
    }

    public string Id { get; init; } = default!;
    public string ContactNumber { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Reference { get; init; } = default!;
    public DateTime StartDate { get; init; } = default!;
    public DateTime EndDate { get; init; } = default!;

    public ICollection<ClientProjectDto> ClientProjects { get; init; } = new List<ClientProjectDto>();

    public ICollection<ContactDto> Contacts { get; init; } = new List<ContactDto>();

    public ICollection<ReportDto> Reports { get; init; } = new List<ReportDto>();

    public ICollection<SowDto> Sows { get; init; } = new List<SowDto>();
}

