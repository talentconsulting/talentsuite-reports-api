using AutoMapper;
using TalentConsulting.TalentSuite.Reports.Common.Entities;
using TalentConsulting.TalentSuite.Reports.Core.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<AuditDto, Audit>();
        CreateMap<ContactDto, Contact>();
        CreateMap<ClientDto, Client>();
        CreateMap<ClientProjectDto, ClientProject>();
        CreateMap<ProjectDto, Project>();
        CreateMap<ProjectRoleDto, ProjectRole>();
        CreateMap<RecipientDto, Recipient>();
        CreateMap<ReportDto, Report>();
        CreateMap<RiskDto, Risk>();
        CreateMap<SowDto, Sow>();
        CreateMap<UserDto, User>();
        CreateMap<UserGroupDto, UserGroup>();
        CreateMap<UserProjectRoleDto, UserProjectRoleDto>();
    }
}
