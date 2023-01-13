using TalentConsulting.TalentSuite.Reports.Common;
using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Core.Entities;
public class Client : EntityBase<string>, IAggregateRoot
{
    private Client() { }

    public Client(string id, string fullname, string contactname, string contactemail)
    {
        Id = id;
        FullName = fullname;
        ContactName = contactname;
        ContactEmail = contactemail;
    }

    public string Id { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string ContactName { get; init; } = default!;
    public string ContactEmail { get; init; } = default!;
}