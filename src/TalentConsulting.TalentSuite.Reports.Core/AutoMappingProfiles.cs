using AutoMapper;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<AuditDto, Audit>().ReverseMap();
        CreateMap<ContactDto, Contact>().ReverseMap();
        CreateMap<ClientDto, Client>().ReverseMap();
        CreateMap<ClientProjectDto, ClientProject>().ReverseMap();
        CreateMap<ProjectDto, Project>().ReverseMap();
        CreateMap<ProjectRoleDto, ProjectRole>().ReverseMap();
        CreateMap<RecipientDto, Recipient>().ReverseMap();
        CreateMap<ReportDto, Report>().ReverseMap();
        CreateMap<RiskDto, Risk>().ReverseMap();
        CreateMap<SowDto, Sow>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserGroupDto, UserGroup>().ReverseMap();
        CreateMap<UserProjectRoleDto, UserProjectRoleDto>().ReverseMap();
    }
}
