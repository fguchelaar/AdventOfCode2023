using AdventKit;

namespace day_16;

public class Puzzle
{
    private readonly Dictionary<Point, char> grid;
    private readonly int height;
    private readonly int width;

    public Puzzle(string input)
    {
        height = input.Trim().Split('\n').Length;
        width = input.Trim().Split('\n')[0].Trim().Length;
        grid = input.Trim().Split('\n')
            .Select((line, row) => (line, row))
            .Aggregate(new Dictionary<Point, char>(), (acc, x) =>
            {
                var (line, row) = x;
                foreach (var (ch, col) in line.Trim().Select((ch, col) => (ch, col)).Where(x => x.ch != '.'))
                    acc[new Point(col, row)] = ch;
                return acc;
            });
    }

    public int Part1() => Solve((Point.Zero, Point.Zero.Right));

    public int Part2()
    {
        IEnumerable<int> solves =
            Enumerable.Range(0, height).SelectMany(y => new int[] {
                    Solve((new Point(0, y), Point.Zero.Right)),
                    Solve((new Point(width - 1, y), Point.Zero.Left))
                    })
            .Concat(
                Enumerable.Range(0, width).SelectMany(x => new int[] {
                    Solve((new Point(x, 0), Point.Zero.Down)),
                    Solve((new Point(x, height - 1), Point.Zero.Up))
                    }));

        return solves.Max();
    }

    private int Solve((Point pos, Point dir) startBeam)
    {
        var beams = new List<(Point pos, Point dir)>
        {
            startBeam
        };

        var visited = new List<(Point pos, Point dir)>();
        while (beams.Count > 0)
        {
            var beam = beams.First();
            beams.RemoveAt(0);

            if (beam.pos.X < 0 || beam.pos.X >= width
                || beam.pos.Y < 0 || beam.pos.Y >= height)
            {
                continue;
            }

            if (visited.Contains((beam.pos, beam.dir)))
            {
                continue;
            }

            visited.Add(beam);
            if (!grid.ContainsKey(beam.pos))
            {
                beams.Add((beam.pos + beam.dir, beam.dir));
            }
            else
            {
                switch (grid[beam.pos])
                {
                    case '|':
                        if (beam.dir == Point.Zero.Up || beam.dir == Point.Zero.Down)
                        {
                            beams.Add((beam.pos + beam.dir, beam.dir));
                        }
                        else
                        {
                            beams.Add((beam.pos + Point.Zero.Up, Point.Zero.Up));
                            beams.Add((beam.pos + Point.Zero.Down, Point.Zero.Down));
                        }
                        break;
                    case '-':
                        if (beam.dir == Point.Zero.Left || beam.dir == Point.Zero.Right)
                        {
                            beams.Add((beam.pos + beam.dir, beam.dir));
                        }
                        else
                        {
                            beams.Add((beam.pos + Point.Zero.Left, Point.Zero.Left));
                            beams.Add((beam.pos + Point.Zero.Right, Point.Zero.Right));
                        }
                        break;
                    case '/':
                        if (beam.dir == Point.Zero.Up)
                        {
                            beams.Add((beam.pos + Point.Zero.Right, Point.Zero.Right));
                        }
                        else if (beam.dir == Point.Zero.Right)
                        {
                            beams.Add((beam.pos + Point.Zero.Up, Point.Zero.Up));
                        }
                        else if (beam.dir == Point.Zero.Down)
                        {
                            beams.Add((beam.pos + Point.Zero.Left, Point.Zero.Left));
                        }
                        else if (beam.dir == Point.Zero.Left)
                        {
                            beams.Add((beam.pos + Point.Zero.Down, Point.Zero.Down));
                        }
                        break;
                    case '\\':
                        if (beam.dir == Point.Zero.Up)
                        {
                            beams.Add((beam.pos + Point.Zero.Left, Point.Zero.Left));
                        }
                        else if (beam.dir == Point.Zero.Right)
                        {
                            beams.Add((beam.pos + Point.Zero.Down, Point.Zero.Down));
                        }
                        else if (beam.dir == Point.Zero.Down)
                        {
                            beams.Add((beam.pos + Point.Zero.Right, Point.Zero.Right));
                        }
                        else if (beam.dir == Point.Zero.Left)
                        {
                            beams.Add((beam.pos + Point.Zero.Up, Point.Zero.Up));
                        }
                        break;
                    default:
                        throw new Exception($"Unknown char {grid[beam.pos]}");
                }
            }
        }
        return visited.Select(b => b.pos).Distinct().Count();
    }
}

