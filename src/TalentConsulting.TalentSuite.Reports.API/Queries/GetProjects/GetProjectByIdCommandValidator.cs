using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetProjects;

public class GetProjectByIdCommandValidator : AbstractValidator<GetProjectByIdCommand>
{
    public GetProjectByIdCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty();
    }

}

