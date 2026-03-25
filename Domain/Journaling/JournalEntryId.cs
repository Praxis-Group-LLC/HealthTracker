namespace Domain.Journaling;

public readonly record struct JournalEntryId(Guid Value)
{
    public static JournalEntryId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}