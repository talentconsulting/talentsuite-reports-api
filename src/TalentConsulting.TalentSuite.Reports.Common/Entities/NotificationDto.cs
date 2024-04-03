using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

[ExcludeFromCodeCoverage]
public record NotificationDto
{
    private NotificationDto() { }

    public NotificationDto(string id, DateTime created, String content, DateTime nextretrydate, string title, string status, ICollection<RecipientDto> recipients)
    {
        Id = id;
        Created = created;
        NextRetryDate = nextretrydate;
        Content = content;
        Title = title;
        Status = status;
        Recipients = recipients;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public DateTime NextRetryDate { get; init; } = default!;
    public string Content { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Status { get; init; } = default!;
    public ICollection<RecipientDto> Recipients { get; } = new List<RecipientDto>();
}

