using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

public interface IUpdateUserCommand
{
    string Id { get; }
    UserDto UserDto { get; }
}
