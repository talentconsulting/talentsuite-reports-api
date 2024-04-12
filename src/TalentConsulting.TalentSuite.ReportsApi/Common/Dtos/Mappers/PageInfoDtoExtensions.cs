using TalentConsulting.TalentSuite.ReportsApi.Db;
using TalentConsulting.TalentSuite.ReportsApi.Db.Entities;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos;

public static class PageInfoDtoExtensions
{
    public static PageInfoDto ToPageInfoDto(this PagedResults<Report> pagedResults)
    {
        return new PageInfoDto(
            pagedResults.TotalCount,
            pagedResults.Page,
            pagedResults.PageSize,
            pagedResults.First,
            pagedResults.Last
        );
    }
}

