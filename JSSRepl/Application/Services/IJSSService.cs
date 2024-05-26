namespace Application.Services;

/// <summary>
/// A service for executing strings as JavaScript using the JSS engine. <br/>
/// An instance of this service contain a JSS VM that it will hold for its lifetime. <br/>
/// This means any variables/functions defined will persist until a new instance is created.
/// </summary>
public interface IJSSService
{
    /// <summary>
    /// Executes the provided string <paramref name="scriptCode"/> as JavaScript code in the JSS engine.
    /// </summary>
    /// <param name="script">The JavaScript script to be executed by JSS.</param>
    /// <returns>A string that represents the result from executing the script.</returns>
    public string ExecuteStringAsJavaScript(string scriptCode);
}
