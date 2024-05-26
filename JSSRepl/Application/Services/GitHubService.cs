using Octokit;

namespace Application.Services;

/// <inheritdoc cref="IGitHubService"/>
public sealed class GitHubService : IGitHubService
{
    public GitHubService(IGitHubClient gitHubClient)
    {
        _gitHubClient = gitHubClient;
    }

    /// <inheritdoc/>
    public async Task<GitHubCommit> GetLatestCommitAsync()
    {
        var branch = await _gitHubClient.Repository.Branch.Get(JSS_OWNER, JSS_REPO_NAME, JSS_MASTER_BRANCH);
        return await _gitHubClient.Repository.Commit.Get(JSS_OWNER, JSS_REPO_NAME, branch.Commit.Sha);
    }

    const string JSS_OWNER = "PrestonLTaylor";
    const string JSS_REPO_NAME = "JSS";
    const string JSS_MASTER_BRANCH = "master";

    private readonly IGitHubClient _gitHubClient;
}
