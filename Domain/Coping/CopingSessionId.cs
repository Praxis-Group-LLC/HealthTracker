using System.Diagnostics.CodeAnalysis;

namespace Domain.Coping;

public readonly record struct CopingSessionId(Guid Value) : IParsable<CopingSessionId>
{
    public static CopingSessionId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();

    public static CopingSessionId Parse(string s, IFormatProvider? provider)
    {
        return new CopingSessionId(Guid.Parse(s));
    } 

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out CopingSessionId result)
    {
        if (!Guid.TryParse(s, out var guid))
        {
            result = default;
            return false;
        }
        
        result = new CopingSessionId(guid);
        return true;
    }
}