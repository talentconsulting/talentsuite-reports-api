using TalentConsulting.TalentSuite.Reports.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;


public interface IUpdateReportCommand
{
    string Id { get; }
    ReportDto ReportDto { get; }
}

