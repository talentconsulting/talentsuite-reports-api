using FluentValidation;
using TalentConsulting.TalentSuite.Reports.API.Commands.CreateReport;

namespace TalentConsulting.TalentSuite.Reports.API.Commands.UpdateReport;

public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
        RuleFor(v => v.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ReportDto)
            .NotNull();

        RuleFor(v => v.ReportDto.Id)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ReportDto.PlannedTasks)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ReportDto.ProjectId)
            .MinimumLength(1)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.ReportDto.UserId)
            .MinimumLength(1)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();
    }
}