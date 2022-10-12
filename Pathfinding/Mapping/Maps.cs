using System.Text.RegularExpressions;
using Pathfinding.Config;
using Pathfinding.Exceptions.Maps;
using Pathfinding.Models;

namespace Pathfinding.Mapping;

/// <summary>
/// A class used to parse map files (.txt) into Map classes. An example of a map (15x15) would be:
/// <para>- - # - - - - - - - - - - - -<br/>
/// - S # - - - - - - - - - - - -<br/>
/// - # # - - - - - - - - - - - -<br/>
/// - - - - - - # # # - - - - - -<br/>
/// - - - - - - # - - - - # - - -<br/>
/// - - - - - - # - - - - # - - -<br/>
/// - - - - - - # - - - - # - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - E - - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - - - - - -<br/>
/// - - - - - - # - - - - - - - -</para>
/// </summary> 
public static class Maps
{
    /// <summary>
    /// Parse a given map file and retrieve the map's size, start point, end point and blocked points.
    /// </summary>
    /// <param name="path">The path to the map file.</param>
    /// <param name="settings">The settings used for the parsing.</param>
    /// <returns>The parsed map from the text at the given path.</returns>
    /// <exception cref="MapFormatException">Thrown when the file at the given path does not match the map format.</exception>
    /// <exception cref="DuplicateMapIdentifierException">Thrown when multiple start or end identifiers are found.</exception>
    /// <exception cref="IdentifierNotFoundException">Thrown when no start or end identifiers are found.</exception>
    public static Map Parse(string path, CharacterSettings settings)
    {
        var lines = File.ReadAllLines(path);
        
        // Check if the input we got for the map matches a specified
        // format so we can parse it
        ValidateInput(lines, settings);

        var map = new Map
        {
            Size = lines.Length,
            Start = new Point(-1, -1),
            End = new Point(-1, -1),
            Blocked = new List<Point>()
        };

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var chars = line.Replace(" ", "");

            for (var j = 0; j < chars.Length; j++)
            {
                var item = chars[j];
                
                if (item == settings.StartKey)
                {
                    // If there are multiple start identifiers, we do not know which one should
                    // be used, so we throw an exception here.
                    if (map.Start.X != -1) throw new DuplicateMapIdentifierException(settings.StartKey);
                    
                    map.Start = new Point(j, i);
                    continue;
                }
                
                if (item == settings.EndKey)
                {
                    // We are doing the same thing as above, if there are multiple end identifiers
                    // we do not know which one should be used.
                    if (map.End.X != -1) throw new DuplicateMapIdentifierException(settings.EndKey);
                    
                    map.End = new Point(j, i);
                    continue;
                }

                if (item == settings.BlockedKey)
                    map.Blocked.Add(new Point(j, i));
            }
        }

        // Throw an error if no identifiers could be found, since the pathfinder will not have a start or end point.
        if (map.Start.X == -1) throw new IdentifierNotFoundException(settings.StartKey);
        if (map.End.X == -1) throw new IdentifierNotFoundException(settings.EndKey);

        return map;
    }

    /// <summary>
    /// Validates a collection of strings by a map format.
    /// </summary>
    /// <param name="lines">The lines to be validated.</param>
    /// <param name="settings">The settings used for the validation.</param>
    /// <exception cref="MapFormatException">Thrown when the file at the given path does not match the map format.</exception>
    private static void ValidateInput(IReadOnlyCollection<string> lines, CharacterSettings settings)
    {
        var text = string.Join("\r\n", lines);

        // Example regex with '30' as length and character options as '-|S|E|#':
        // (?:(?:(-|S|#|E)\s){29}(-|S|#|E)((\r\n)|$)){30}
        string Escape (char ch) => Regex.Escape(ch.ToString());

        var charOptions = $@"{Escape(settings.EmptyKey)}|{Escape(settings.StartKey)}|{Escape(settings.EndKey)}|{Escape(settings.BlockedKey)}";
        var pattern = $"(?:(?:({charOptions})\\s){{{lines.Count - 1}}}({charOptions})((\\r\\n)|$)){{{lines.Count}}}";

        if (!Regex.IsMatch(text, pattern))
            throw new MapFormatException(text, pattern);
    }
}