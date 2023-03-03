using FluentValidation;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.UserDto)
            .NotNull();

        RuleFor(v => v.UserDto.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.UserDto.Firstname)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.UserDto.Lastname)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.UserDto.Email)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.UserDto.UserGroupId)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();
    }
}
