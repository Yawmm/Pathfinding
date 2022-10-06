using Pathfinding.Models;

namespace Pathfinding.Algorithms;

public static class Pathfinding
{
    /// <summary>
    /// Find the path that the pathfinding algorithm used to go from the starting node,
    /// to the ending node.
    /// </summary>
    /// <param name="start">The starting node.</param>
    /// <param name="current">The current (goal) node.</param>
    /// <returns></returns>
    public static IEnumerable<Node> Backtrack(Node start, Node? current)
    {
        // Initialize a new list of nodes representing a path.
        var path = new List<Node>();
        
        // We can now loop until there is no previous node, which means we have reached
        // the starting node.
        while (current is not null && current != start)
        {
            // Since we are going from front to back, we can't just .Add() it, we need to 
            // insert the path at the beginning.
            path.Insert(0, current);
            current = current.Previous;
        }
        
        // We have reached the starting node above, but we haven't added the starting node to
        // the path, so we do that here.
        path.Insert(0, start);

        return path;
    }

    /// <summary>
    /// Get an instance of an IPathFinder by a given pathfinder type.
    /// </summary>
    /// <param name="type">The type of the pathfinder.</param>
    /// <returns>An instance of an IPathfinder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the given Pathfinder type does not have a corresponding IPathfinder instance.</exception>
    public static IPathfinder GetPathfinder(Pathfinder type)
        => type switch
        {
            Pathfinder.Dijkstra => new Dijkstra(),
            Pathfinder.AStar => new AStar(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    /// <summary>
    /// Find a path from a start node, to an end node, given that these nodes have their
    /// corresponding edges added.
    /// </summary>
    /// <param name="graph">The graph used to find a path on.</param>
    /// <typeparam name="T">The pathfinder used for finding the path.</typeparam>
    /// <returns>A list of nodes representing the found path.</returns>
    public static IEnumerable<Node> FindPath<T>(Graph graph)
        where T : IPathfinder, new()
        => FindPath<T>(graph.Nodes, graph.Start, graph.End);
    
    /// <summary>
    /// Find a path from a start node, to an end node, given that these nodes have their
    /// corresponding edges added.
    /// </summary>
    /// <param name="nodes">The nodes in the map which will need to be reset after finding the path.</param>
    /// <param name="from">The starting node.</param>
    /// <param name="to">The ending node.</param>
    /// <typeparam name="T">The pathfinder used for finding the path.</typeparam>
    /// <returns>A list of nodes representing the found path.</returns>
    public static IEnumerable<Node> FindPath<T>(Node[,] nodes, Node from, Node to)
        where T : IPathfinder, new()
    {
        // Find the path.
        var pathfinder = new T();
        var path = pathfinder.FindPath(from, to);
        
        // Reset all nodes, since they can be mutated during the pathfinding.
        foreach (var node in nodes)
            node.Reset();

        return path;
    }

    /// <summary>
    /// Find a path from a start node, to an end node, given that these nodes have their
    /// corresponding edges added.
    /// </summary>
    /// <param name="pathfinder">The pathfinder used for finding the path.</param>
    /// <param name="nodes">The nodes in the map which will need to be reset after finding the path.</param>
    /// <param name="from">The starting node.</param>
    /// <param name="to">The ending node.</param>
    /// <returns>A list of nodes representing the found path.</returns>
    public static IEnumerable<Node> FindPath(IPathfinder pathfinder, Node[,] nodes, Node from, Node to)
    {
        var path = pathfinder.FindPath(from, to);
        
        // Reset all nodes, since they can be mutated during the pathfinding.
        foreach (var node in nodes)
            node.Reset();

        return path;
    }
}