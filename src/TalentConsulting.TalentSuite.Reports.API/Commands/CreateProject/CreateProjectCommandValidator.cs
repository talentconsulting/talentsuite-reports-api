using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(v => v.ProjectDto)
            .NotNull();

        RuleFor(v => v.ProjectDto.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ProjectDto.ContactNumber)
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
