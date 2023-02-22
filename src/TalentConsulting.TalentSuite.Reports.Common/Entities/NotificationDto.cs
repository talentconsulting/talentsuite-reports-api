using System.Globalization;

namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record NotificationDto
{
    private NotificationDto() { }

    public NotificationDto(string id, DateTime created, String content, DateTime nextretrydate, string title, int status)
    {
        Id = id;
        Created = created;
        NextRetryDate = nextretrydate;
        Content = content;
        Title = title;
        Status = status;
    }

    public string Id { get; init; } = default!;
    public DateTime Created { get; init; } = default!;
    public DateTime NextRetryDate { get; init; } = default!;
    public string Content { get; init; } = default!;
    public string Title { get; init; } = default!;
    public int Status { get; init; } = default!;
}

