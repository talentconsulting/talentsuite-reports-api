using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

internal class RiskDtoValidator : AbstractValidator<RiskDto>
{
    public RiskDtoValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty();
        RuleFor(dto => dto.Description).NotEmpty();
    }
}
