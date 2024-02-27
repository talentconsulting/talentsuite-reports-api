using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("contacts")]
public class Contact : IAggregateRoot
{
    private Contact() { }

    public Contact(string id, string firstname, string email, bool receivesreport, string projectId)
    {
        if (Guid.TryParse(id, out Guid guidId))
        {
            Id = guidId;
        }
        else
        {
            throw new ArgumentException("Invalid Guid", nameof(id));
        }
        Firstname = firstname;
        Email = email;
        ReceivesReport = receivesreport;
        ProjectId = projectId;
    }

    public Guid Id { get; set; }
    public string Firstname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool ReceivesReport { get; set; } = default!;
    public string ProjectId { get; set; } = default!;
#if ADD_ENTITY_NAV
    public virtual Project Project { get; set; } = null!;
#endif

}

