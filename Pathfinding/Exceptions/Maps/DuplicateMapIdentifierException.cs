namespace Pathfinding.Exceptions.Maps;

public class DuplicateMapIdentifierException : Exception
{
    public DuplicateMapIdentifierException(char identifier)
        : base($"Found multiple of the identifier '{identifier}' in the given map.")
    { }
}