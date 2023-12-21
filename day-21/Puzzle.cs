using AdventKit;

namespace day_21;

public class Puzzle(string input)
{
    private readonly string input = input;

    public void PrintMap(IDictionary<Point, char> map, int width, int height)
    {
        var minX = map.Keys.Min(p => p.X);
        var maxX = map.Keys.Max(p => p.X);
        var minY = map.Keys.Min(p => p.Y);
        var maxY = map.Keys.Max(p => p.Y);

        Console.WriteLine("");
        Console.WriteLine($"minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
        Console.WriteLine($"Count: {map.Count(x => x.Value == 'O')}");

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (map.ContainsKey(new Point(x, y)))
                {
                    Console.Write(map[new Point(x, y)]);
                }
                else
                {
                    Console.Write(map[Normalize(new Point(x, y), width, height)]);
                }
            }

            Console.WriteLine();
        }

    }
    public int Part1(int steps = 64)
    {
        var map = input.Trim().Split('\n').SelectMany((row, y) => row.Trim().Select((c, x) => new KeyValuePair<Point, char>(new Point(x, y), c))).ToDictionary(x => x.Key, x => x.Value);

        var startPoint = map.First(x => x.Value == 'S').Key;
        map[startPoint] = 'O';

        for (int i = 0; i < steps; i++)
        {
            var possibleLocations = map
                .Where(x => x.Value == 'O')
                .Select(x => x.Key);

            var newLocations = possibleLocations.SelectMany(x => new[] { x.Up, x.Right, x.Down, x.Left })
                .Where(x => map.ContainsKey(x) && map[x] != '#')
                .ToHashSet();

            // leave the current locations
            foreach (var location in possibleLocations)
            {
                map[location] = '.';
            }

            // mark new locations
            foreach (var location in newLocations)
            {
                map[location] = 'O';
            }
        }

        return map.Count(x => x.Value == 'O');
    }

    public int Part2(int steps = 26_501_365)
    {
        var map = input.Trim().Split('\n').SelectMany((row, y) => row.Trim().Select((c, x) => new KeyValuePair<Point, char>(new Point(x, y), c))).ToDictionary(x => x.Key, x => x.Value);
        var width = map.Keys.Max(p => p.X) + 1;
        var height = map.Keys.Max(p => p.Y) + 1;

        var startPoint = map.First(x => x.Value == 'S').Key;
        map[startPoint] = 'O';

        for (int i = 0; i < steps; i++)
        {
            if (i % 1_000 == 0)
            {
                Console.WriteLine($"Step {i}");
            }
            var possibleLocations = map
                .Where(x => x.Value == 'O')
                .Select(x => x.Key);

            var newLocations = possibleLocations.SelectMany(x => new[] { x.Up, x.Right, x.Down, x.Left })
                .Where(x =>
                {
                    var normalized = Normalize(x, width, height);
                    return map.ContainsKey(normalized) && map[normalized] != '#';
                })
                .ToHashSet();

            // leave the current locations
            foreach (var location in possibleLocations)
            {
                map[location] = '.';
            }

            // mark new locations
            foreach (var location in newLocations)
            {
                map[location] = 'O';
            }
            // PrintMap(map, width, height);
        }

        return map.Count(x => x.Value == 'O');
    }

    public static Point Normalize(Point point, int width, int height)
    {
        return new Point(((point.X % width) + width) % width,
            ((point.Y % height) + height) % height);
    }
}
