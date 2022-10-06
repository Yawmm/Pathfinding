using Pathfinding.Models;

namespace Pathfinding.Config;

/// <summary>
/// A class representing the available application settings
/// </summary>
public class Settings
{
    /// <summary>
    /// The settings related to pathfinding.
    /// </summary>
    public PathfindingSettings Pathfinding { get; set; }
    
    /// <summary>
    /// The settings related to maps.
    /// </summary>
    public MapSettings Map { get; set; }
}

/// <summary>
/// A class representing the pathfinding settings.
/// </summary>
public class PathfindingSettings
{
    /// <summary>
    /// The type of pathfinder to be used.
    /// </summary>
    public Pathfinder Type { get; set; }
}

/// <summary>
/// A class representing the map settings.
/// </summary>
public class MapSettings
{
    /// <summary>
    /// The path to the map file.
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// The map settings related to parsing.
    /// </summary>
    public CharacterSettings Parser { get; set; }

    /// <summary>
    /// The map settings related to printing/displaying.
    /// </summary>
    public CharacterSettings Printer { get; set; }
}

/// <summary>
/// A class representing the available map settings.
/// </summary>
public class CharacterSettings
{
    /// <summary>
    /// The character of the start identifier.
    /// </summary>
    public char StartKey { get; set; }
    
    /// <summary>
    /// The character of the end identifier.
    /// </summary>
    public char EndKey { get; set; }
    
    /// <summary>
    /// The character of the empty identifier.
    /// </summary>
    public char EmptyKey { get; set; }
    
    /// <summary>
    /// The character of the blocked identifier.
    /// </summary>
    public char BlockedKey { get; set; }

    /// <summary>
    /// Turn these settings into a string.
    /// </summary>
    /// <returns>The formatted string.</returns>
    public override string ToString()
        => $"(start = {StartKey}, end = {EndKey}, empty = {EmptyKey}, blocked = {BlockedKey})";
}
