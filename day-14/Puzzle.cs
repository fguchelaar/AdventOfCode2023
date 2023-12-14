using System.Drawing;

namespace day_14;

public class Puzzle
{
    private readonly IDictionary<Point, char> platform;
    private int height;

    public Puzzle(string input)
    {
        height = input.Trim().Split('\n').Length;
        platform = input.Trim().Split('\n')
            .Select((line, row) => (line, row))
            .Aggregate(new Dictionary<Point, char>(), (acc, x) =>
            {
                var (line, row) = x;
                foreach (var (ch, col) in line.Select((ch, col) => (ch, col)).Where(x => x.ch != '.'))
                    acc[new Point(col, row)] = ch;
                return acc;
            });
    }

    public int Part1()
    {
        // All rounded rocks
        var roundedRocks = platform
            .Where(x => x.Value == 'O')
            .Select(x => x.Key)
            .OrderBy(x => x.Y)
            .ToList();

        // tilt north
        foreach (var rock in roundedRocks)
        {
            var newY = rock.Y - 1;
            while (newY > -1 && !platform.ContainsKey(new Point(rock.X, newY)))
                newY--;

            newY++;

            platform.Remove(rock);
            platform[new Point(rock.X, newY)] = 'O';
        }

        Print(platform);

        return platform
            .Where(x => x.Value == 'O')
            .Select(x => height - x.Key.Y)
            .Sum();
    }

    void Print(IDictionary<Point, char> platform)
    {
        var maxX = platform.Max(x => x.Key.X);
        var maxY = platform.Max(x => x.Key.Y);

        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var key = new Point(x, y);
                if (platform.ContainsKey(key))
                {
                    var ch = platform[key];
                    Console.Write(ch);
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }
    }

    public int Part2()
    {
        return -1;
    }
}
