using AdventKit;

namespace day_17;

public class Puzzle
{
    private readonly int[][] grid;
    private readonly int width;
    private readonly int height;

    public Puzzle(string input)
    {
        grid = input.Trim().Split('\n')
            .Select(line => line.Trim().Select(c => int.Parse("" + c)).ToArray()).ToArray();
        width = grid[0].Length;
        height = grid.Length;
    }

    public int Part1()
    {
        // 716: too high

        int h(Step step, Point target)
        {
            // return 1;
            return step.position.Manhattan(target);
        }

        List<Step> ReconstructPath(Dictionary<Step, Step> cameFrom, Step current)
        {
            var totalPath = new List<Step> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            return totalPath;
        }

        var start = new Step { position = Point.Zero, previous = Point.Zero, count = 0 };
        var target = new Point(width - 1, height - 1);

        var open = new PriorityQueue<Step, int>();
        open.Enqueue(start, 0);

        var cameFrom = new Dictionary<Step, Step>();

        var gScore = new Dictionary<Step, int>();
        var fScore = new Dictionary<Step, int>();

        gScore[start] = 0;
        fScore[start] = h(start, target);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            if (current.position == target)
            {
                var path = ReconstructPath(cameFrom, current);
                PrintGrid(path);
                return path.Sum(s => grid[s.position.Y][s.position.X]) - grid[0][0];
            }
            foreach (var neighbor in GetNeighbors(current))
            {
                var tentativeGscore = gScore[current] + grid[neighbor.position.Y][neighbor.position.X];
                var neighborGscore = gScore.GetValueOrDefault(neighbor, int.MaxValue);
                if (tentativeGscore < neighborGscore)
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGscore;
                    fScore[neighbor] = tentativeGscore + h(neighbor, target);
                    if (!open.UnorderedItems.Any(s => s.Element.Equals(neighbor)))
                    {
                        open.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        Console.WriteLine("Came from: " + cameFrom.Count);
        Console.WriteLine($"{cameFrom}");
        return -1;
    }

    public int Part2()
    {

        int h(Step step, Point target)
        {
            // return 1;
            return step.position.Manhattan(target);
        }

        List<Step> ReconstructPath(Dictionary<Step, Step> cameFrom, Step current)
        {
            var totalPath = new List<Step> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            return totalPath;
        }

        var start = new Step { position = Point.Zero, previous = Point.Zero, count = 0 };
        var target = new Point(width - 1, height - 1);

        var open = new PriorityQueue<Step, int>();
        open.Enqueue(start, 0);

        var cameFrom = new Dictionary<Step, Step>();

        var gScore = new Dictionary<Step, int>();
        var fScore = new Dictionary<Step, int>();

        gScore[start] = 0;
        fScore[start] = h(start, target);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            if (current.position == target)
            {
                var path = ReconstructPath(cameFrom, current);
                PrintGrid(path);
                return path.Sum(s => grid[s.position.Y][s.position.X]) - grid[0][0];
            }
            foreach (var neighbor in GetNeighbors2(current))
            {
                var tentativeGscore = gScore[current] + grid[neighbor.position.Y][neighbor.position.X];
                var neighborGscore = gScore.GetValueOrDefault(neighbor, int.MaxValue);
                if (tentativeGscore < neighborGscore)
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGscore;
                    fScore[neighbor] = tentativeGscore + h(neighbor, target);
                    if (!open.UnorderedItems.Any(s => s.Element.Equals(neighbor)))
                    {
                        open.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        Console.WriteLine("Came from: " + cameFrom.Count);
        Console.WriteLine($"{cameFrom}");
        return -1;
    }
    IEnumerable<Step> GetNeighbors(Step step)
    {
        int GetCount(Point pos)
        {
            if (pos.X == step.previous.X || pos.Y == step.previous.Y)
                return step.count + 1;
            else
                return 1;
        }

        return new Point[] { step.position.Up, step.position.Right, step.position.Down, step.position.Left }
            .Where(p => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height)
            .Where(p => p != step.previous)
            .Select(p => new Step { position = p, previous = step.position, count = GetCount(p) })
            .Where(s => s.count <= 3);
    }

    IEnumerable<Step> GetNeighbors2(Step step)
    {
        if (step.count == 0)
        {
            return new Point[] { step.position.Up, step.position.Right, step.position.Down, step.position.Left }
                .Where(p => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height)
                .Select(p => new Step { position = p, previous = step.position, count = 1 });
        }
        else if (step.count < 4)
        {
            return new Point[] { step.position + (step.position - step.previous) }
                .Where(p => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height)
                .Select(p => new Step { position = p, previous = step.position, count = step.count + 1 });
        }
        else
        {
            return new Point[] { step.position.Up, step.position.Right, step.position.Down, step.position.Left }
                .Where(p => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height)
                .Where(p => p != step.previous)
                .Select(p => new Step { position = p, previous = step.position, count = GetCount(p) })
                .Where(s => s.count <= 10);
        }

        int GetCount(Point pos)
        {
            if (pos.X == step.previous.X || pos.Y == step.previous.Y)
                return step.count + 1;
            else
                return 1;
        }
    }

    void PrintGrid(List<Step> path)
    {
        Console.WriteLine($"Path {path.Count}:");
        foreach (var s in path)
        {
            Console.WriteLine($"{s}");
        }

        Console.WriteLine();

        for (int y = 0; y < height; y++)
        {
            var line = "";
            for (int x = 0; x < width; x++)
            {
                var pos = new Point(x, y);
                if (path.Any(s => s.position == pos))
                    line += " ";
                else
                    line += grid[y][x];
            }
            Console.WriteLine(line);
        }
    }
}

struct Step
{
    public Point position;
    public Point previous;
    public int count;

    override public string ToString() => $"({position}, {previous}, {count})";
}
