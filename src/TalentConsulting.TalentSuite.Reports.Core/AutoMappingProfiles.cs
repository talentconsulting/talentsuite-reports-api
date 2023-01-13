using AutoMapper;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<ProjectDto, Project>();
    }
}
