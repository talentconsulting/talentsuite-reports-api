using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

public class Project : EntityBase<string>, IAggregateRoot
{
    private Project() { }

    public Project(string id, string contactNumber, string name, string reference, DateTime startDate, DateTime endDate,
        ICollection<ClientProject> ClientProjects,
        ICollection<Contact> Contacts,
        ICollection<Report> Reports,
        ICollection<Sow> Sows)
    {
        Id = id;
        ContactNumber = contactNumber;
        Name = name;
        Reference = reference;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string ContactNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Reference { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<ClientProject> ClientProjects { get; } = new List<ClientProject>();

    public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual ICollection<Sow> Sows { get; } = new List<Sow>();
}
