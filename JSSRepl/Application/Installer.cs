using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace Application;

public static class Installer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJSSService, JSSService>();

        services.AddTransient<IGitHubClient, GitHubClient>((_) => new GitHubClient(new ProductHeaderValue(JSSREPL_GITHUB_OWNER)));
        services.AddTransient<IGitHubService, GitHubService>();

        services.AddMemoryCache();

        return services;
    }

    /// <summary>
    /// The GitHub user account ascociated with the JSSRepl repository
    /// </summary>
    const string JSSREPL_GITHUB_OWNER = "PrestonLTaylor";
}
