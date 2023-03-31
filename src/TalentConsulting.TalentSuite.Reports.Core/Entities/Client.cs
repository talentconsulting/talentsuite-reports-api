using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;

[Table("cients")]
public class Client : EntityBase<string>, IAggregateRoot
{
    private Client() { }

    public Client(string id, string name, string contactname, string contactemail, ICollection<ClientProject> clientProjects)
    {
        base.id = id;
        Name = name;
        ContactName = contactname;
        ContactEmail = contactemail;
        ClientProjects = clientProjects;
    }
    public string Name { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public virtual ICollection<ClientProject> ClientProjects { get; set; } = new List<ClientProject>();
}