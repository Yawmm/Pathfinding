namespace Pathfinding.Models;

/// <summary>
/// A point in space, represented by an X-coordinate and a Y-coordinate.
/// </summary>
public struct Point
{
    /// <summary>
    /// The X-coordinate of the point.
    /// </summary>
    public int X { get; }
    
    /// <summary>
    /// The Y-coordinate of the point.
    /// </summary>
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Point((int, int) pos)
    {
        X = pos.Item1;
        Y = pos.Item2;
    }

    public override string ToString()
        =>$"({X}, {Y})";

}