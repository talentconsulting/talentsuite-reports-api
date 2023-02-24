using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Contact : EntityBase<string>, IAggregateRoot
{
    private Contact() { }

    public Contact(string id, string firstname, string email, bool receivesreport, string projectId)
    {
        Id = id;
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = projectId;
    }

    public string Firstname { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool ReceivesReport { get; init; } = default!;
    public string ProjectId { get; init; } = default!;
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

}

