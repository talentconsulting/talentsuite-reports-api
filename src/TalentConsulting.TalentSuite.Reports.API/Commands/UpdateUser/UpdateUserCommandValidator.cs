using FluentValidation;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateUser;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.UserDto)
            .NotNull();

        RuleFor(v => v.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

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
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.UserDto.UserGroupId)
            .MinimumLength(1)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();
    }
}
