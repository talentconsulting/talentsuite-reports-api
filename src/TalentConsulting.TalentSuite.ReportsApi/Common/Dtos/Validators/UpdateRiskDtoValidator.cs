using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

internal class UpdateRiskDtoValidator : AbstractValidator<UpdateRiskDto>
{
    public UpdateRiskDtoValidator()
    {
        RuleFor(dto => dto.Description).NotEmpty();
    }
}
