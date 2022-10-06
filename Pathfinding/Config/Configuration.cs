
using Microsoft.Extensions.Configuration;

namespace Pathfinding.Config;

/// <summary>
/// A class used to initialize configurations and to retrieve settings
/// from said configurations.
/// </summary>
public static class Configuration
{
    /// <summary>
    /// Initialize the application configuration from a given path.
    /// </summary>
    /// <param name="path">The path of the application configuration.</param>
    /// <returns>The application settings.</returns>
    public static Settings Get(string path)
    {
        var build = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path)
            .Build();

        // Deserialize the JSON to a usable object.
        var instance = new Settings();
        build.GetRequiredSection("Settings").Bind(instance);
        
        return instance;
    }
}