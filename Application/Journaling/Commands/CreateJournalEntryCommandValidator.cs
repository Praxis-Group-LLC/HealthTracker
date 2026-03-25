using FluentValidation;

namespace Application.Journaling.Commands;

public sealed class CreateJournalEntryCommandValidator 
    : AbstractValidator<CreateJournalEntryCommand>
{
    public CreateJournalEntryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("User is required.");

        RuleFor(x => x.Date)
            .NotEqual(default(DateOnly))
            .WithMessage("Date is required.");

        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Journal text is required.")
            .MaximumLength(4000)
            .WithMessage("Journal text cannot exceed 4000 characters.");

        RuleFor(x => x.MoodRating)
            .InclusiveBetween(1, 10)
            .WithMessage("Mood rating, must be between 1 and 10.");

        RuleFor(x => x.SleepHours)
            .InclusiveBetween(0, 24)
            .WithMessage("Sleep hours must be between 0 and 24.");
    }
}