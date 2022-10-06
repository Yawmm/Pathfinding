using Pathfinding.Config;
using Pathfinding.Models;

namespace Pathfinding.Printing;

/// <summary>
/// A class used to print out node graphs and related items.
/// </summary>
public static class Print
{
    /// <summary>
    /// Print out a summary of the ran pathfinder.
    /// </summary>
    /// <param name="nodes">The nodes to be printed.</param>
    /// <param name="path">The, optional, path that has been taken from start to end.</param>
    /// <param name="start">The starting node.</param>
    /// <param name="end">The ending node.</param>
    /// <param name="settings">The settings used for printing.</param>
    public static void Summary(Node[,] nodes, List<Node> path, Node start, Node end, Settings settings)
    {
        for (var i = 0; i < nodes.GetLength(0); i++)
            Console.Write("──");

        Console.WriteLine("\nSummary:\n");

        Console.WriteLine($"Used algorithm: {settings.Pathfinding.Type}");
        Console.WriteLine($"Map path: {settings.Map.Path}");
        Console.WriteLine($"Map parsing keys: {settings.Map.Parser}");
        Console.WriteLine($"Map printer keys: {settings.Map.Printer}");
        
        Console.WriteLine($"\nStarting point: {start}");
        Console.WriteLine($"Ending point: {end}");
        Console.Write($"\nRaw path ({path.Count} steps): {start}");
        var pointStrings = path.Select(n => n.ToString()).ToList();
        for (var i = 1; i < pointStrings.Count; i++)
        {
            Console.Write($" → {pointStrings[i]}");
            if (i % 10 == 0)
                Console.WriteLine("\t");
        }
    }
    
    /// <summary>
    /// Print out a representation of graph of nodes to the console.
    /// </summary>
    /// <param name="nodes">The nodes to be printed.</param>
    /// <param name="path">The, optional, path that has been taken from start to end.</param>
    /// <param name="start">The starting node.</param>
    /// <param name="end">The ending node.</param>
    /// <param name="settings">The settings used for printing.</param>
    public static void Nodes(Node[,] nodes, ICollection<Node>? path, Node start, Node end, CharacterSettings settings)
    {
        for (var i = 0; i < nodes.GetLength(0); i++)
        {
            for (var j = 0; j < nodes.GetLength(1); j++)
            {
                var node = nodes[j, i];
                
                // This can use a better system, but this isn't a gigantic project so this will
                // be fine.
                if (node.Blocked) Console.Write(settings.BlockedKey);
                else if (node == start) Console.Write(settings.StartKey);
                else if (node == end) Console.Write(settings.EndKey);
                else if (path is not null && path.Contains(node))
                {
                    PathNode(path, node);
                    continue;
                }
                else Console.Write(settings.EmptyKey);
                
                // Provides spacing between each node.
                Console.Write(" ");
            }

            // Provides spacing between each row.
            Console.WriteLine("");
        }
    }

    /// <summary>
    /// Print out a representation of a node on a given path.
    /// </summary>
    /// <param name="path">The path used.</param>
    /// <param name="current">The node that is on the given path.</param>
    private static void PathNode(ICollection<Node> path, Node current)
    {
        var index = path.ToList().IndexOf(current);
        
        // Retrieve the next and previous nodes so we can get fancy unicode
        // characters.
        var next = path.ElementAt(index + 1);
        var prev = index > 0 ? path.ElementAt(index - 1) : null;

        // The directions of the previous and next nodes, relative to the current node.
        var pd = GetDirection(current, prev);
        var nd = GetDirection(current, next);

        switch (pd)
        {
            // - - -
            // * N *
            // - - -
            case null when nd == Direction.Right:
            case null when nd == Direction.Left:
            case Direction.Left when nd == Direction.Right:
            case Direction.Right when nd == Direction.Left:
                Console.Write("──");
                break;
            
            // - * -
            // - N -
            // - * -
            case null when nd == Direction.Down:
            case null when nd == Direction.Up:
            case Direction.Up when nd == Direction.Down:
            case Direction.Down when nd == Direction.Up:
                Console.Write("│ ");
                break;
            
            // - * -
            // - N *
            // - - -
            case Direction.Right when nd == Direction.Up:
            case Direction.Up when nd == Direction.Right:
                Console.Write("└─");
                break;
            
            // - - -
            // - N *
            // - * -
            case Direction.Right when nd == Direction.Down:
            case Direction.Down when nd == Direction.Right:
                Console.Write("┌─");
                break;
            
            // - * -
            // * N -
            // - - -
            case Direction.Left when nd == Direction.Up:
            case Direction.Up when nd == Direction.Left:
                Console.Write("┘ ");
                break;
            
            // - - -
            // * N -
            // - * -
            case Direction.Left when nd == Direction.Down:
            case Direction.Down when nd == Direction.Left:
                Console.Write("┐ ");
                break;
        }
    }

    /// <summary>
    /// Get the direction of node b, relative to node a.
    /// </summary>
    /// <param name="a">The node to be compared to.</param>
    /// <param name="b">The node being compared.</param>
    /// <returns>The direction of node b, relative to node a.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when both nodes are at the exact same position.</exception>
    private static Direction? GetDirection(Node a, Node? b)
    {
        if (b is null) return null;
        
        if (b.Position.Y > a.Position.Y) return Direction.Down;
        if (b.Position.Y < a.Position.Y) return Direction.Up;
        
        if (b.Position.X > a.Position.X) return Direction.Right;
        if (b.Position.X < a.Position.X) return Direction.Left;

        throw new ArgumentOutOfRangeException();
    }
}