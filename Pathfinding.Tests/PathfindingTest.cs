using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Pathfinding.Algorithms;
using Pathfinding.Config;
using Pathfinding.Mapping;
using Pathfinding.Models;

namespace Algorithms.Tests;

/// <summary>
/// The test parameters for the A* pathfinding algorithm.
/// </summary>
internal class AStarTests : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestFixtureParameters(typeof(AStar), "appsettings_15x15.json", 31, 15, (1, 1), (10, 8));
        yield return new TestFixtureParameters(typeof(AStar), "appsettings_30x30.json", 52, 30, (1, 0), (23, 29));
        yield return new TestFixtureParameters(typeof(AStar), "appsettings_50x50.json", 76, 50, (1, 0), (29, 47));
    }
}

/// <summary>
/// The test parameters for the Dijkstra pathfinding algorithm.
/// </summary>
internal class DijkstraTests : IEnumerable 
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestFixtureParameters(typeof(Dijkstra), "appsettings_15x15.json", 31, 15, (1, 1), (10, 8));
        yield return new TestFixtureParameters(typeof(Dijkstra), "appsettings_30x30.json", 52, 30, (1, 0), (23, 29));
        yield return new TestFixtureParameters(typeof(Dijkstra), "appsettings_50x50.json", 76, 50, (1, 0), (29, 47));
    }
}

/// <summary>
/// A test class used to test basic functionality of the application, like parsing maps, creating graphs and finding paths.
/// </summary>
/// <typeparam name="TAlgorithm">The type of algorithm to use.</typeparam>
[TestFixtureSource(typeof(AStarTests))]
[TestFixtureSource(typeof(DijkstraTests))]
public class PathfindingTest<TAlgorithm> 
    where TAlgorithm : IPathfinder, new()
{
    private readonly Map _map;
    private readonly Graph _graph;
    private readonly List<Node> _path;
    
    private readonly int _pathLength;
    private readonly int _mapSize;
    private readonly Point _start;
    private readonly Point _end;

    public PathfindingTest(string path, int pathLength, int mapSize, (int, int) start, (int, int) end)
    {
        var settings = Configuration.Get(path);
        
        _map = Maps.Parse(settings.Map.Path, settings.Map.Parser);
        _graph = Graphs.CreateGraph(_map);
        
        _path = Pathfinding.Algorithms.Pathfinding.FindPath<TAlgorithm>(_graph).ToList();
        
        _pathLength = pathLength;
        _mapSize = mapSize;
        
        _start = new Point(start);
        _end = new Point(end);
    }

    #region Path tests

    [Test]
    public void PathLength()
    {
        Assert.That(_path.Count, Is.EqualTo(_pathLength));
    }

    #endregion

    #region Map tests

    [Test]
    public void MapSize()
    {
        Assert.That(_map.Size, Is.EqualTo(_mapSize));
    }
    

    [Test]
    public void MapStart()
    {
        Assert.That(_map.Start, Is.EqualTo(_start));
    }
    
    [Test]
    public void MapEnd()
    {
        Assert.That(_map.End, Is.EqualTo(_end));
    }

    #endregion

    #region Graph tests

    [Test]
    public void GraphNodesLength()
    {
        Assert.That(_graph.Nodes.Length, Is.EqualTo(Math.Pow(_map.Size, 2)));
    }
    
    [Test]
    public void MapGraphStart()
    {
        Assert.That(_map.Start, Is.EqualTo(_graph.Start.Position));
    }
    
    [Test]
    public void MapGraphEnd()
    {
        Assert.That(_map.End, Is.EqualTo(_graph.End.Position));
    }

    #endregion
}