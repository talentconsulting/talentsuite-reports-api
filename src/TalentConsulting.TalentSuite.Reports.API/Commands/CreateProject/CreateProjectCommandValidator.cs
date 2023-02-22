using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;

public class CreateReferralCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateReferralCommandValidator()
    {
        RuleFor(v => v.ProjectDto)
            .NotNull();

        RuleFor(v => v.ProjectDto.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ProjectDto.ClId)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ProjectDto.Name)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ProjectDto.Reference)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ProjectDto.StartDate)
            .NotNull();

        RuleFor(v => v.ProjectDto.EndDate)
            .NotNull();
    }
}
