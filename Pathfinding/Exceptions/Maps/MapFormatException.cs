namespace Pathfinding.Exceptions.Maps;

public class MapFormatException : Exception
{
    public MapFormatException(string map, string regex)
        : base($"Given value did not meet regex constraints of '{regex}': \n\n{map}\n")
    { }
}