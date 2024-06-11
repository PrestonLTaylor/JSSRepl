using Domain;
using JSS.Common;
using JSS.Lib;
using JSS.Lib.Execution;
using Microsoft.Extensions.Logging;
using SyntaxErrorException = JSS.Lib.SyntaxErrorException;

namespace Application.Services;

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
    public async Task<ExecutionResult> ExecuteStringAsJavaScriptAsync(string scriptCode, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("{Script} was provided by user, attempting to execute.", scriptCode);

        try
        {
            var task = Task.Run(() => TryToExecuteStringAsJavaScript(scriptCode, cancellationToken));
            return await task.WaitAsync(cancellationToken);
        }
        catch (SyntaxErrorException ex)
        {
            return new ExecutionResult($"Uncaught {ex.Message}", IsNormalCompletion: false);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "A script execution timed out.");
            return new ExecutionResult($"Timeout Error! Reason: A script took too long to execute.", IsNormalCompletion: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected exception was raised by JSS.");
            return new ExecutionResult($"Internal Error! Reason: {ex.Message}", IsNormalCompletion: false);
        }
    }

    /// <inheritdoc cref="ExecuteStringAsJavaScriptAsync(string)"/>
    private ExecutionResult TryToExecuteStringAsJavaScript(string scriptCode, CancellationToken cancellationToken = default)
    {
        var parser = new Parser(scriptCode);
        var script = parser.Parse(_vm);
        var completion = script.ScriptEvaluation(cancellationToken);
        var value = Print.CompletionToString(_vm, completion);
        return new ExecutionResult(value, completion.IsNormalCompletion());
    }

    private readonly ILogger<JSSService> _logger;
    private readonly VM _vm;
}
