using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

public class ReportDtoValidator : AbstractValidator<ReportDto>
{
    public ReportDtoValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty();
        RuleFor(dto => dto.ClientId).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.SowId).NotEmpty();
        RuleForEach(dto => dto.Risks).SetValidator(new RiskDtoValidator());
    }
}
