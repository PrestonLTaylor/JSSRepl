using Octokit;

namespace Application.Services;

/// <summary>
/// A service for retrieving commit information from the JSS GitHub repository.
/// </summary>
public interface IGitHubService
{
    /// <summary>
    /// Retrieves information on the latest commit from the master branch.
    /// </summary>
    /// <returns>A task containing the latest commit from the master branch.</returns>
    public Task<GitHubCommit> GetLatestCommitAsync();
}
