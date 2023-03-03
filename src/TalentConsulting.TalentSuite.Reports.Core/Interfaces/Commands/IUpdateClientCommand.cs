using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

public interface IUpdateClientCommand
{
    string Id { get; }
    ClientDto ClientDto { get; }
}
