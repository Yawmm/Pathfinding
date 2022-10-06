using Pathfinding.Config;
using Pathfinding.Mapping;
using Pathfinding.Printing;

namespace Pathfinding;

/// <summary>
/// The main class in the application, which runs everything.
/// </summary>
internal static class Program
{
    public static void Main()
    {
        // Get the settings and the map dictated by said settings
        var settings = Configuration.Get("appsettings.json");
        var map = Maps.Parse(settings.Map.Path, settings.Map.Parser);
        
        // Generate a usable graph from the map, with all the nodes and the start and end nodes.
        var (nodes, start, end) = Graphs.CreateGraph(map);
        
        // Find the path from the starting node to the ending node.
        var pathfinder = Algorithms.Pathfinding.GetPathfinder(settings.Pathfinding.Type);
        var path = Algorithms.Pathfinding.FindPath(pathfinder, nodes, start, end).ToList();
        
        // Print the results
        Print.Nodes(nodes, path, start, end, settings.Map.Printer);
        Print.Summary(nodes, path, start, end, settings);
    }
}