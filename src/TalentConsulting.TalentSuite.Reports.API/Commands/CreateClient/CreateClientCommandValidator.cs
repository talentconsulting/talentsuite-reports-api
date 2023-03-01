using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(v => v.ClientDto)
            .NotNull();

        RuleFor(v => v.ClientDto.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ClientDto.Name)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ClientDto.ContactName)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ClientDto.ContactEmail)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();
    }
}
