using TalentConsulting.TalentSuite.Reports.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Reports.Infrastructure.Service;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}

