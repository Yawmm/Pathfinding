namespace Pathfinding.Exceptions.Maps;

public class IdentifierNotFoundException : Exception
{
    public IdentifierNotFoundException(char identifier)
        : base($"Could not find identifier '{identifier}' in the given map.")
    { }
}