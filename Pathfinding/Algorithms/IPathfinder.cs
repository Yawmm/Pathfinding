using Pathfinding.Models;

namespace Pathfinding.Algorithms;

/// <summary>
/// An interface that represents entity which can find a path between two nodes,
/// a starting node and an ending node (the goal).
/// </summary>
public interface IPathfinder
{
    /// <summary>
    /// Find a path from a start node, to an end node, given that these nodes have their
    /// corresponding edges added.
    /// </summary>
    /// <param name="start">The node which is the start of the path.</param>
    /// <param name="goal">The node which will be the end of the path.</param>
    /// <returns>A list of nodes representing the found path.</returns>
    IEnumerable<Node> FindPath(Node start, Node goal);
}