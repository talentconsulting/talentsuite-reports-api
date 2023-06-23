using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(v => v.ClientDto)
            .NotNull();

        RuleFor(v => v.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

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
