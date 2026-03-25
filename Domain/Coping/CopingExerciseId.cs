namespace Domain.Coping;

public readonly record struct CopingExerciseId(Guid Value)
{
    public static CopingExerciseId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}