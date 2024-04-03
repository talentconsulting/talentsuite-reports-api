using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record RecipientDto
{
    private RecipientDto() { }

    public RecipientDto(string id, string name, string email, string notificationId)
    {
        Id = id;
        Name = name;
        Email = email;
        Notificationid = notificationId;
    }

    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Notificationid { get; init; } = default!;
#if ADD_ENTITY_NAV
    public NotificationDto Notification { get; set; } = null!;
#endif
}