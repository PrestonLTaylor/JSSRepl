using JSS.Common;
using JSS.Lib;
using JSS.Lib.Execution;
using Microsoft.Extensions.Logging;
using SyntaxErrorException = JSS.Lib.SyntaxErrorException;

namespace Presentation.Server.Services;

/// <inheritdoc cref="IJSSService"/>
public sealed class JSSService : IJSSService
{
    /// <summary>
    /// Creates an instance of JSSService by creating the VM to be used for executing provided strings.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if we failed to initialize the JSS VM.</exception>
    public JSSService(ILogger<JSSService> logger)
    {
        _logger = logger;

        var initializeResult = Realm.InitializeHostDefinedRealm(out _vm);
        if (initializeResult.IsAbruptCompletion())
        {
            throw new InvalidOperationException($"Failed to initialize the JSS engine: {Print.CompletionToString(_vm, initializeResult)}.");
        }
    }

    /// <inheritdoc/>
    public string ExecuteStringAsJavaScript(string scriptCode)
    {
        _logger.LogInformation("{Script} was provided by user, attempting to execute.", scriptCode);

        try
        {
            var parser = new Parser(scriptCode);
            var script = parser.Parse(_vm);
            var completion = script.ScriptEvaluation();
            return Print.CompletionToString(_vm, completion);
        }
        catch (SyntaxErrorException ex)
        {
            return $"Uncaught {ex.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected exception was raised by JSS.");
            return $"Internal Error! Reason: {ex}";
        }
    }

    private readonly ILogger<JSSService> _logger;
    private readonly VM _vm;
}
