namespace Domain;

/// <summary>
/// Represents the result of executing JavaScript from the JSS engine.
/// </summary>
/// <param name="Value">The result of execution as a string.</param>
/// <param name="IsNormalCompletion"><see langword="true"/> if execution was a normal completion, <see langword="false"/> otherwise.</param>
public record struct ExecutionResult(string Value, bool IsNormalCompletion);
