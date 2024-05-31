namespace Domain;

/// <summary>
/// Represents an item to be displayed in the REPL console.
/// </summary>
/// <param name="Value">The content to be displayed in the REPL console.</param>
/// <param name="IsError">If the JSS execution was an abrupt completion then the item should be an error.</param>
public record struct ConsoleItem(string Value, bool IsError);
