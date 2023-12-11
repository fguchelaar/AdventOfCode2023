using AdventKit;

namespace day_10;

record Pipe(char Symbol)
{
    public IEnumerable<Point> Adjacent(Point at)
    {
        return Symbol switch
        {
            'L' => [at.Up, at.Right],
            'J' => [at.Up, at.Left],
            '|' => [at.Up, at.Down],
            '-' => [at.Left, at.Right],
            '7' => [at.Left, at.Down],
            'F' => [at.Right, at.Down],
            'S' => [],
            '.' => [],
            _ => throw new Exception($"Unknown pipe: {Symbol}")
        };
    }
}

public class Puzzle
{
    private readonly Dictionary<Point, Pipe> grid;

    private readonly IList<Point> path = new List<Point>();

    public Puzzle(string input)
    {
        grid = input.Trim().Split('\n')
            .Select((line, row) => (line, row))
            .Aggregate(new Dictionary<Point, Pipe>(), (acc, x) =>
            {
                var (line, row) = x;
                foreach (var (ch, col) in line.Select((ch, col) => (ch, col)))
                    acc[new Point(col, row)] = new Pipe(ch);
                return acc;
            });

        path = FindPath(grid.First(p => p.Value.Symbol == 'S').Key).ToList();
    }

    public int Part1() => path.Count / 2;

    public int Part2()
    {
        var minX = path.Min(p => p.X);
        var maxX = path.Max(p => p.X);
        var minY = path.Min(p => p.Y);
        var maxY = path.Max(p => p.Y);

        int count = 0;
        for (var y = minY; y <= maxY; y++)
        {
            bool isIn = false;
            for (var x = minX; x <= maxX; x++)
            {
                var point = new Point(x, y);

                if (path.Contains(point))
                {
                    var symbol = grid[point].Symbol;
                    if (symbol == '|' || symbol == 'F' || symbol == '7')
                    {
                        isIn = !isIn;
                    }
                }
                else
                {
                    if (isIn)
                    {
                        count++;
                    }
                }

            }
        }
        return count;
    }

    private IEnumerable<Point> FindPath(Point startPoint)
    {
        var path = new List<Point> { startPoint };
        var startPipe = DeterminePipe(startPoint);
        grid[startPoint] = startPipe;
        var previous = startPoint;
        var current = startPipe.Adjacent(startPoint).First();
        path.Add(current);
        while (current != startPoint)
        {
            var pipe = grid[current];
            var next = pipe.Adjacent(current).First(p => p != previous);
            previous = current;
            current = next;
            path.Add(current);
        }

        return path;
    }

    private Pipe DeterminePipe(Point startPoint)
    {
        var up = grid.TryGetValue(startPoint.Up, out var upPipe) ? upPipe : null;
        var down = grid.TryGetValue(startPoint.Down, out var downPipe) ? downPipe : null;
        var left = grid.TryGetValue(startPoint.Left, out var leftPipe) ? leftPipe : null;
        var right = grid.TryGetValue(startPoint.Right, out var rightPipe) ? rightPipe : null;

        if (up != null && up.Adjacent(startPoint.Up).Contains(startPoint)
            && right != null && right.Adjacent(startPoint.Right).Contains(startPoint))
        {
            return new Pipe('L');
        }
        else if (up != null && up.Adjacent(startPoint.Up).Contains(startPoint)
            && down != null && down.Adjacent(startPoint.Down).Contains(startPoint))
        {
            return new Pipe('|');
        }
        else if (up != null && up.Adjacent(startPoint.Up).Contains(startPoint)
            && left != null && left.Adjacent(startPoint.Left).Contains(startPoint))
        {
            return new Pipe('J');
        }
        else if (right != null && right.Adjacent(startPoint.Right).Contains(startPoint)
            && down != null && down.Adjacent(startPoint.Down).Contains(startPoint))
        {
            return new Pipe('F');
        }
        else if (right != null && right.Adjacent(startPoint.Right).Contains(startPoint)
            && left != null && left.Adjacent(startPoint.Left).Contains(startPoint))
        {
            return new Pipe('-');
        }
        else if (down != null && down.Adjacent(startPoint.Down).Contains(startPoint)
            && left != null && left.Adjacent(startPoint.Left).Contains(startPoint))
        {
            return new Pipe('7');
        }
        else
        {
            throw new Exception($"Unknown pipe at {startPoint}");
        }
    }
}
