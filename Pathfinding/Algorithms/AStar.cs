using Pathfinding.Exceptions.Algorithms;
using Pathfinding.Models;

namespace Pathfinding.Algorithms;

/// <summary>
/// A pathfinder used to find a path between two nodes on a map.
/// This implements the A* algorithm: see 'https://en.wikipedia.org/wiki/A*_search_algorithm'.
/// This algorithm is basically a super-set of the Dijkstra algorithm, <see cref="Dijkstra"/>
/// </summary>
public class AStar : IPathfinder
{
    /// <summary>
    /// The weight of each line, since this is an easy example the weight
    /// of each line will be equal to this number.
    /// </summary>
    private const int LINE_WEIGHT = 1;

    /// <summary>
    /// Find a path from a start node, to an end node, given that these nodes have their
    /// corresponding edge nodes added.
    /// </summary>
    /// <param name="start">The node which is the start of the path.</param>
    /// <param name="goal">The node which will be the end of the path.</param>
    /// <returns>A list of nodes representing the found path.</returns>
    /// <exception cref="PathNotFoundException">Thrown if no path could be found from the starting node, to the ending node.</exception>
    public IEnumerable<Node> FindPath(Node start, Node goal)
    {
        // The set containing all the nodes that have not been searched
        // through.
        var open = new List<Node> { start };
        
        // Since there are no nodes preceding the starting node, we can reset its GScore.
        start.GScore = 0;
        start.FScore = Heuristic(start, goal);

        // Only if there are open nodes can we check new nodes.
        while (open.Count > 0)
        {
            // Get the node with the lowest FScore.
            var current = open.MinBy(n => n.FScore)!;
            
            // If the node we are comparing to is equal to the goal, we know 
            // we have reached said goal and we can generate the path.
            if (current == goal)
                return Pathfinding.Backtrack(start, current);

            // We can now safely remove the current node from all open
            // nodes since we are searching through it, and its edges
            // right now.
            open.Remove(current);
            
            // If the node is blocked, we can safely ignore the node.
            if (current.Blocked)
                continue;

            // When we have checked if the current node isn't equal to the goal, we can check
            // their edges.
            foreach (var edge in current.Edges)
            {
                // This score represents the best path through the current node to the goal.
                var tentativeScore = current.GScore + LINE_WEIGHT;
                
                // If the tentativeGScore is higher than the score of the edge, we know the
                // edge is not closer to the goal, so we can continue.
                if (tentativeScore >= edge.GScore) continue;

                // A closer node has been found! We can now add this node to the best paths we have.
                edge.Previous = current;
                edge.GScore = tentativeScore;
                
                // This is one of the two differences between the Dijkstra algorithm
                // We use an FScore, representing the score from the starting node to the previous node, plus
                // the distance between this neighbouring node and the goal.
                edge.FScore = tentativeScore
                                   + Heuristic(edge, goal)
                                   // Breaking ties by changing values a little bit each time, significantly
                                   // reduces amount of nodes searched.
                                   + Cross(current, start, goal) * 0.001;
                
                // Now that this node is closer to the goal, we can re-add it to the open set if it isn't
                // already in said open set.
                if (open.Contains(edge)) continue;
                open.Add(edge);
            }
        }

        // The open set is empty and no node has reached the goal, this means that the goal is not
        // reachable.
        throw new PathNotFoundException(start, goal);
    }
    
    /// <summary>
    /// Generates a heuristic for the first node compared to the second node.
    /// See: 'http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html#S7'
    /// </summary>
    /// <param name="a">The first node.</param>
    /// <param name="b">The second node.</param>
    /// <returns>The heuristic used with two nodes.</returns>
    private double Heuristic(Node a, Node b)
    {
        var dx = Math.Abs(b.Position.X - a.Position.X);
        var dy = Math.Abs(b.Position.Y - a.Position.Y);

        return dx + dy;
    }
    
    /// <summary>
    /// Calculate the cross product between three nodes.
    /// </summary>
    /// <param name="a">The first node.</param>
    /// <param name="b">The second node.</param>
    /// <param name="c">The third node.</param>
    /// <returns>The cross product of the three nodes.</returns>
    private double Cross(Node a, Node b, Node c)
    {
        var dx1 = a.Position.X - c.Position.X;
        var dy1 = a.Position.Y - c.Position.Y;
        
        var dx2 = b.Position.X - c.Position.X;
        var dy2 = b.Position.Y - c.Position.Y;
        
        return Math.Abs(dx1 * dy2 - dx2 * dy1);
    }
}