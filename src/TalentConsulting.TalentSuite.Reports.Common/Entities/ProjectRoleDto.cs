using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record ProjectRoleDto
{
    private ProjectRoleDto() { }

    public ProjectRoleDto(string id, string name, bool technical, string description)
    {
        Id = id;
        Name = name;
        Technical = technical;
        Description = description;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public bool Technical { get; init; } = default!;
    public string Description { get; init; } = default!;
}

