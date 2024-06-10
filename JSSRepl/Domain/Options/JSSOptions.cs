namespace Domain.Options;

public sealed class JSSOptions
{
    public const string Section = "JSSExecution";
    private const int DefaultTimeoutMilliseconds = 10000;

    public int ScriptTimeoutMilliseconds { get; set; } = DefaultTimeoutMilliseconds;
}
