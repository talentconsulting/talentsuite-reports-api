using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Contact : EntityBase<string>, IAggregateRoot
{
    private Contact() { }

    public Contact(string id, string firstname, string email, bool receivesreport, int startdate)
    {
        Id = id;
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = startdate;
    }

    public string Firstname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReport { get; init; } = default!;
    public int ProjectId { get; init; } = default!;
}

