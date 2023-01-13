using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

public interface ICreateProjectCommand
{
    ProjectDto ProjectDto { get; }
}
