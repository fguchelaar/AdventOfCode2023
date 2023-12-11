
namespace day_11;

/// <summary>
/// A point in the universe as a class, so it has mutable properties in a 
/// foreach iteration.
class Point(long x, long y)
{
    public long X { get; set; } = x;
    public long Y { get; set; } = y;
}

public class Puzzle(string input)
{
    public long Part1() =>
       Solve(2);

    public long Part2() =>
        Solve(1_000_000);

    /// <summary>
    /// Expand the universe by the given factor, and calculate the sum of the 
    /// distances between all pairs of galaxies.
    /// </summary>
    public long Solve(long expandBy)
    {
        var galaxies = ExpandUniverse(expandBy);
        long sum = 0;
        // calculate the distance between each pair of galaxies
        foreach (var first in galaxies.Select((g, i) => (g, i)))
        {
            foreach (var second in galaxies.Skip(first.i))
            {
                var distance = Math.Abs(first.g.X - second.X) + Math.Abs(first.g.Y - second.Y);
                sum += distance;
            }
        }
        return sum;
    }

    List<Point> ExpandUniverse(long expandBy = 2)
    {
        var galaxies = input.Trim().Split("\n")
            .SelectMany((line, y) => line.Select((c, x) => (c, x, y)))
            .Where(p => p.c == '#')
            .Select(p => new Point(p.x, p.y))
            .ToList();

        // expand the universe where every row or column is empty
        var minX = galaxies.Min(p => p.X);
        var maxX = galaxies.Max(p => p.X);
        var minY = galaxies.Min(p => p.Y);
        var maxY = galaxies.Max(p => p.Y);

        // horizontal
        for (var x = maxX; x > minX; x--)
        {
            // is there a galaxy in this column?
            if (!galaxies.Any(p => p.X == x))
            {
                // move all right-side galaxies one place to the right
                foreach (var galaxy in galaxies.Where(p => p.X > x))
                {
                    galaxy.X += expandBy - 1;
                }
            }
        }

        // vertical
        for (var y = maxY; y > minY; y--)
        {
            // is there a galaxy in this column?
            if (!galaxies.Any(p => p.Y == y))
            {
                // move all right-side galaxies one place to the right
                foreach (var galaxy in galaxies.Where(p => p.Y > y))
                {
                    galaxy.Y += expandBy - 1;
                }
            }
        }
        return galaxies;
    }
}
