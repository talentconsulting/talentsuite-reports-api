using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

internal class ReportDtoValidator : AbstractValidator<ReportDto>
{
    public ReportDtoValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty();
        RuleFor(dto => dto.ClientId).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.SowId).NotEmpty();
        RuleFor(dto => dto.Risks).NotNull();
        RuleForEach(dto => dto.Risks).NotNull().SetValidator(new RiskDtoValidator());
    }
}
