using Pathfinding.Models;

namespace Pathfinding.Exceptions.Algorithms;

public class PathNotFoundException : Exception
{
    public PathNotFoundException(Node from, Node to)
        : base($"Could not find a path between nodes '{from.ToString()}' and '{to.ToString()}'.")
    { }
}