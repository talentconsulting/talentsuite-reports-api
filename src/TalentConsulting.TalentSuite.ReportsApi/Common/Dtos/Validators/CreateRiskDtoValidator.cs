using FluentValidation;

namespace TalentConsulting.TalentSuite.ReportsApi.Common.Dtos.Validators;

public class CreateRiskDtoValidator : AbstractValidator<CreateRiskDto>
{
    public CreateRiskDtoValidator()
    {
        RuleFor(dto => dto.Description).NotEmpty();
    }
}
