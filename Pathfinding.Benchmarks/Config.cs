using BenchmarkDotNet.Attributes;
using Pathfinding.Config;
using Pathfinding.Mapping;
using Pathfinding.Models;

namespace Algorithms.Benchmarks;

/// <summary>
/// A support class used to get data related to configuration of the benchmarks.
/// </summary>
public static class Config
{
    /// <summary>
    /// Get a graph from a settings path.
    /// </summary>
    /// <param name="settingsPath">The path to the settings.</param>
    /// <returns>The graph dictated by the given settings.</returns>
    public static Graph GetGraph(string settingsPath)
    {
        var settings = Configuration.Get(settingsPath);
        var map = Maps.Parse(settings.Map.Path, settings.Map.Parser);
        
        return Graphs.CreateGraph(map);
    }
}