using BenchmarkDotNet.Attributes;
using Pathfinding.Algorithms;
using Pathfinding.Models;

namespace Algorithms.Benchmarks;

/// <summary>
/// A class used to run a range of A* benchmarks with maps that are empty.
/// </summary>
[RPlotExporter]
[CsvMeasurementsExporter]
[MemoryDiagnoser(false)]
public class EmptyAStarBenchmarks
{
    private const string KEY = "Empty";
    
    /// <summary>
    /// Benchmark with a graph of 100 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath10() 
        => Pathfinding.Algorithms.Pathfinding.FindPath<AStar>(
            Config.GetGraph($"{KEY}/appsettings_10x10.json")
        ).ToList();

    /// <summary>
    /// Benchmark with a graph of 10.000 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath100()
        => Pathfinding.Algorithms.Pathfinding.FindPath<AStar>(
            Config.GetGraph($"{KEY}/appsettings_100x100.json")
        ).ToList();
    
    /// <summary>
    /// Benchmark with a graph of 1.000.000 nodes.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public List<Node> FindPath1000()
        => Pathfinding.Algorithms.Pathfinding.FindPath<AStar>(
            Config.GetGraph($"{KEY}/appsettings_1000x1000.json")
        ).ToList();
}