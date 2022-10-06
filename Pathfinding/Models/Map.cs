namespace Pathfinding.Models;

/// <summary>
/// A class used to store information about the Node graph.
/// </summary>
public class Map
{
    /// <summary>
    /// The size of the map.
    /// </summary>
    public int Size { get; init; }
    
    /// <summary>
    /// The starting point on the map.
    /// </summary>
    public Point Start { get; set; }
    
    /// <summary>
    /// The ending point on the map.
    /// </summary>
    public Point End { get; set; }

    /// <summary>
    /// The points on the map which are blocked off.
    /// </summary>
    public List<Point> Blocked { get; init; } = new();
}