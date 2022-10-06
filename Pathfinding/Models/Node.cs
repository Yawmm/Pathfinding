namespace Pathfinding.Models;

/// <summary>
/// A class used to store information about a Node on a graph.
/// </summary>
public class Node : IComparable<Node>
{
    /// <summary>
    /// The 2D-position of the node.
    /// </summary>
    public Point Position { get; }
    
    /// <summary>
    /// The previous node found while pathfinding.
    /// </summary>
    public Node? Previous { get; set; }
    
    /// <summary>
    /// The nodes which neighbour this specific node.
    /// </summary>
    public IEnumerable<Node> Edges { get; }

    /// <summary>
    /// The relative cost of the cheapest path from start to this specific node.
    /// </summary>
    public double GScore { get; set; } = float.MaxValue;
    
    /// <summary>
    /// The best guess as to how much the cheapest path from start to goal will cost, if it goes
    /// through this specific node.
    /// </summary>
    public double FScore { get; set; } = float.MaxValue;

    /// <summary>
    /// Whether or not the node is blocked off.
    /// </summary>
    public bool Blocked { get; set; }
    
    public Node(Point point, IEnumerable<Node> edges)
    {
        Position = point;
        Edges = edges;
    }

    /// <summary>
    /// Reset the node's state.
    /// </summary>
    public void Reset()
    {
        Previous = null;
        GScore = float.MaxValue;
        FScore = float.MaxValue;
    }

    /// <summary>
    /// Compares the current node with another, and determines which has a higher priority.
    /// </summary>
    /// <param name="obj">The node to be compared to.</param>
    /// <returns>An integer representing the finished comparison.</returns>
    public int CompareTo(Node? obj)
        => FScoreComparer(this, obj);
    
    public static int FScoreComparer (Node a, Node b)
    {
        return a.FScore > b.FScore
            ? 1
            : a.FScore < b.FScore
                ? -1
                : 0;
    }
    
    public static int GScoreComparer (Node a, Node b)
    {
        return a.GScore > b.GScore
            ? 1
            : a.GScore < b.GScore
                ? -1
                : 0;
    }

    /// <summary>
    /// Turn this specific node into a string.
    /// </summary>
    /// <returns>The formatted string.</returns>
    public override string ToString()
        => Position.ToString();
}