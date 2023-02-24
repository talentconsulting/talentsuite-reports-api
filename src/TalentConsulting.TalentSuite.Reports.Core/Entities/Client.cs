using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Client : EntityBase<string>, IAggregateRoot
{
    private Client() { }

    public Client(string id, string name, string contactname, string contactemail)
    {
        Id = id;
        Name = name;
        ContactName = contactname;
        ContactEmail = contactemail;
    }
    public string Name { get; init; } = null!;
    public string ContactName { get; init; } = null!;
    public string ContactEmail { get; init; } = null!;
    public virtual ICollection<ClientProject> ClientProjects { get; } = new List<ClientProject>();
}