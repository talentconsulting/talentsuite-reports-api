using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

public interface IUpdateProjectCommand
{
    string Id { get; }
    ProjectDto ProjectDto { get; }
}
