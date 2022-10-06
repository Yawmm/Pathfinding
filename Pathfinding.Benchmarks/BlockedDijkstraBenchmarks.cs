using BenchmarkDotNet.Attributes;
using Pathfinding.Algorithms;
using Pathfinding.Models;

namespace Algorithms.Benchmarks;

/// <summary>
/// A class used to run a range of Dijkstra benchmarks with maps that have blockers in them.
/// </summary>
[RPlotExporter]
[CsvMeasurementsExporter]
[MemoryDiagnoser(false)]
public class BlockedDijkstraBenchmarks
{
    private const string KEY = "Blocked";
    
    /// <summary>
    /// Benchmark with a graph of 225 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath15() 
        => Pathfinding.Algorithms.Pathfinding.FindPath<Dijkstra>(
            Config.GetGraph($"{KEY}/appsettings_15x15.json")
        ).ToList();

    /// <summary>
    /// Benchmark with a graph of 900 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath30()
        => Pathfinding.Algorithms.Pathfinding.FindPath<Dijkstra>(
            Config.GetGraph($"{KEY}/appsettings_30x30.json")
        ).ToList();
    
    /// <summary>
    /// Benchmark with a graph of 2.500 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath50()
        => Pathfinding.Algorithms.Pathfinding.FindPath<Dijkstra>(
            Config.GetGraph($"{KEY}/appsettings_50x50.json")
        ).ToList();
    
    /// <summary>
    /// Benchmark with a graph of 2.500 nodes, with a harder map.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath50Walled()
        => Pathfinding.Algorithms.Pathfinding.FindPath<Dijkstra>(
            Config.GetGraph($"{KEY}/appsettings_50x50_walled.json")
        ).ToList();
}