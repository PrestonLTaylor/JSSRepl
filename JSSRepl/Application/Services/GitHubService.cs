using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace Application.Services;

/// <inheritdoc cref="IGitHubService"/>
public sealed class GitHubService : IGitHubService
{
    public GitHubService(IGitHubClient gitHubClient, IMemoryCache cache)
    {
        _gitHubClient = gitHubClient;
        _cache = cache;
    }

    /// <inheritdoc/>
    public async Task<GitHubCommit> GetBuildCommitAsync()
    {
        return await _cache.GetOrCreateAsync(CACHE_KEY, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = CACHE_LIFETIME;
            return await GetLatestCommitAsync();
        })
            ?? throw new InvalidOperationException("Failed to get GitHub commit of JSS build.");
    }

    private async Task<GitHubCommit> GetLatestCommitAsync()
    {
        var branch = await _gitHubClient.Repository.Branch.Get(JSS_OWNER, JSS_REPO_NAME, JSS_MASTER_BRANCH);
        return await _gitHubClient.Repository.Commit.Get(JSS_OWNER, JSS_REPO_NAME, branch.Commit.Sha);
    }

    const string JSS_OWNER = "PrestonLTaylor";
    const string JSS_REPO_NAME = "JSS";
    const string JSS_MASTER_BRANCH = "master";
    const string CACHE_KEY = "COMMIT_KEY";
    private readonly TimeSpan CACHE_LIFETIME = TimeSpan.FromMinutes(30);

    private readonly IGitHubClient _gitHubClient;
    private readonly IMemoryCache _cache;
}
