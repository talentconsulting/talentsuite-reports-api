using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

public class CreateReportDtoValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportDtoValidator()
    {
        RuleFor(dto => dto.ClientId).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.SowId).NotEmpty();
        RuleForEach(dto => dto.Risks).SetValidator(new CreateRiskDtoValidator());
    }
}
