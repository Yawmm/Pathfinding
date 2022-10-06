using Pathfinding.Models;

namespace Pathfinding.Mapping;

/// <summary>
/// A class used to generate graphs of nodes from an input of a map.
/// </summary>
public static class Graphs
{
    /// <summary>
    /// Create a graph of nodes from a given map.
    /// </summary>
    /// <param name="map">The map used to generate the nodes.</param>
    /// <returns>A list representing the given map as a 2D-array of nodes.</returns>
    public static Graph CreateGraph(Map map)
    {
        // Create the new 2D-array of nodes with the given map size.
        var nodes = new Node[map.Size, map.Size];

        // Loop through each dimension of the 2D-array.
        for (var i = 0; i < nodes.GetLength(0); i++)
        for (var j = 0; j < nodes.GetLength(1); j++)
        {
            // The coordinates of the node.
            var point = new Point(i, j);
                
            // Create a new node with its neighbours.
            var neighbours = GetNeighbours(nodes, point);
            nodes[i, j] = new Node(point, neighbours);
        }

        // Get the start- and ending node
        var start = nodes[map.Start.X, map.Start.Y];
        var end = nodes[map.End.X, map.End.Y];

        // Set the blocked nodes.
        foreach (var blocked in map.Blocked)
            nodes[blocked.X, blocked.Y].Blocked = true;
        
        return new Graph(nodes, start, end);
    }

    /// <summary>
    /// Get all neighbours of a given point (2-4 neighbours).
    /// </summary>
    /// <param name="nodes">The list of nodes representing the map used.</param>
    /// <param name="point">The coordinates of the point in the map which needs its neighbours.</param>
    /// <returns>A list of nodes representing all the neighbours of the given point.</returns>
    private static IEnumerable<Node> GetNeighbours(Node[,] nodes, Point point)
    {
        var (x, y) = (point.X, point.Y);
        
        // Left and down nodes.
        if (x > 0) yield return nodes[x - 1, y];
        if (y > 0) yield return nodes[x, y - 1];
        
        // Up and right nodes.
        if (x < nodes.GetLength(0) - 1) yield return nodes[x + 1, y];
        if (y < nodes.GetLength(1) - 1) yield return nodes[x, y + 1];
    }
}