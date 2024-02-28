namespace TalentConsulting.TalentSuite.Reports.Common.Entities;

public record SowFileDto
{
    public required string Id { get; init; }
    public required string Mimetype { get; init; }
    public required string Filename { get; init; }
    public required int Size { get; init; }
    public required string SowId { get; init; }
    public required byte[] File { get; init; }
}
