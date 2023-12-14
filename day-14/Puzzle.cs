using System.Drawing;

namespace day_14;

public class Puzzle
{
    private readonly IDictionary<Point, char> platform;
    private int height;
    private int width;

    public Puzzle(string input)
    {
        height = input.Trim().Split('\n').Length;
        width = input.Trim().Split('\n')[0].Trim().Length;
        platform = input.Trim().Split('\n')
            .Select((line, row) => (line, row))
            .Aggregate(new Dictionary<Point, char>(), (acc, x) =>
            {
                var (line, row) = x;
                foreach (var (ch, col) in line.Trim().Select((ch, col) => (ch, col)).Where(x => x.ch != '.'))
                    acc[new Point(col, row)] = ch;
                return acc;
            });
        // Print(platform);
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

        Console.WriteLine($"{maxX}x{maxY}. O={platform.Count(x => x.Value == 'O')} #={platform.Count(x => x.Value == '#')}");

        for (var y = 0; y <= maxY; y++)
        {
            Console.Write($"{y,2} ");

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
        Console.WriteLine();

    }

    public int Part2()
    {
        Console.WriteLine(Cycle(122));

        var rest = (1_000_000_000 - 122) % 22;

        Console.WriteLine(Cycle(rest));
        // Cycle(1_000_000_000);
        return -1;
    }

    public int Cycle(int times)
    {
        for (int i = 0; i < times; i++)
        {
            // Up
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
                // Console.WriteLine("NORTH");
                // Print(platform);
            }
            // Left
            {
                // All rounded rocks
                var roundedRocks = platform
                    .Where(x => x.Value == 'O')
                    .Select(x => x.Key)
                    .OrderBy(x => x.X)
                    .ToList();

                // tilt north
                foreach (var rock in roundedRocks)
                {
                    var newX = rock.X - 1;
                    while (newX > -1 && !platform.ContainsKey(new Point(newX, rock.Y)))
                        newX--;

                    newX++;

                    platform.Remove(rock);
                    platform[new Point(newX, rock.Y)] = 'O';
                }
                // Console.WriteLine("WEST");
                // Print(platform);
            }
            // Down
            {
                // All rounded rocks
                var roundedRocks = platform
                    .Where(x => x.Value == 'O')
                    .Select(x => x.Key)
                    .OrderByDescending(x => x.Y)
                    .ToList();

                // tilt north
                foreach (var rock in roundedRocks)
                {
                    var newY = rock.Y + 1;
                    while (newY < height && !platform.ContainsKey(new Point(rock.X, newY)))
                        newY++;

                    newY--;

                    platform.Remove(rock);
                    platform[new Point(rock.X, newY)] = 'O';
                }
                // Console.WriteLine("SOUTH");
                // Print(platform);
            }
            // Left
            {
                // All rounded rocks
                var roundedRocks = platform
                    .Where(x => x.Value == 'O')
                    .Select(x => x.Key)
                    .OrderByDescending(x => x.X)
                    .ToList();

                // tilt north
                foreach (var rock in roundedRocks)
                {
                    var newX = rock.X + 1;
                    while (newX < width && !platform.ContainsKey(new Point(newX, rock.Y)))
                        newX++;

                    newX--;

                    platform.Remove(rock);
                    platform[new Point(newX, rock.Y)] = 'O';
                }
                // Console.WriteLine("EAST");
                // Print(platform);
            }
            // Print sum
            // var sum = platform
            // .Where(x => x.Value == 'O')
            // .Select(x => height - x.Key.Y)
            // .Sum();
            // Console.WriteLine($"{sum}");
        }

        // Print(platform);

        return platform
            .Where(x => x.Value == 'O')
            .Select(x => height - x.Key.Y)
            .Sum();
    }
}
