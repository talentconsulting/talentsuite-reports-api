using System.Globalization;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record RecipientDto
{
    private RecipientDto() { }

    public RecipientDto(string id, string name, string email, int notificationid)
    {
        Id = id;
        Name = name;
        Email = email;
        Notificationid = notificationid;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public int Notificationid { get; init; } = default!;

}