using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("contacts")]
public class Contact : EntityBase<string>, IAggregateRoot
{
    private Contact() { }

    public Contact(string id, string firstname, string email, bool receivesreport, string projectId)
    {
        base.id = id;
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = projectId;
    }

    public string Firstname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool ReceivesReport { get; set; } = default!;
    public string ProjectId { get; set; } = default!;
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

}

