using AdventKit;

namespace day_18;

public class Puzzle(string input)
{
    private readonly IEnumerable<(char dir, int cnt, string color)> instructions =
        input.Trim().Split("\n")
            .Select(line => line.Trim().Split(new char[] { ' ', '(', ')', '#' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(line => (line[0][0], int.Parse(line[1]), line[2]));

    public long Part1()
    {
        // Convert the instructions to a list of vertices
        List<Point> vertices;
        vertices = [];
        var pos = new Point(0, 0);
        foreach (var instruction in instructions)
        {
            var (dir, cnt, _) = instruction;
            var (dx, dy) = dir switch
            {
                'R' => (cnt, 0),
                'L' => (-cnt, 0),
                'U' => (0, -cnt),
                'D' => (0, cnt),
                _ => throw new Exception("Unknown direction")
            };
            pos = new Point(pos.X + dx, pos.Y + dy);
            vertices.Add(pos);
        }

        return CalculateArea(vertices) + CalculatePerimeter(vertices) / 2 + 1;
    }

    public long Part2()
    {
        (char dir, int cnt) Decode(string color)
        {
            var chucnk = color[..5];
            int distance = Convert.ToInt32(color[..5], 16);
            char direction = color[5] switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                '3' => 'U',
                _ => throw new Exception("Unknown direction")
            };

            return (direction, distance);
        }

        // Convert the instructions to a list of vertices
        List<Point> vertices;
        vertices = [];
        var pos = new Point(0, 0);
        foreach (var instruction in instructions)
        {
            var (dir, cnt) = Decode(instruction.color);
            var (dx, dy) = dir switch
            {
                'R' => (cnt, 0),
                'L' => (-cnt, 0),
                'U' => (0, -cnt),
                'D' => (0, cnt),
                _ => throw new Exception("Unknown direction")
            };
            pos = new Point(pos.X + dx, pos.Y + dy);
            vertices.Add(pos);
        }

        return CalculateArea(vertices) + CalculatePerimeter(vertices) / 2 + 1;
    }

    public long CalculatePerimeter(List<Point> vertices)
    {
        int numVertices = vertices.Count;
        int perimeter = 0;

        for (int i = 0; i < numVertices; i++)
        {
            int j = (i + 1) % numVertices;
            perimeter += Math.Abs(vertices[i].X - vertices[j].X) + Math.Abs(vertices[i].Y - vertices[j].Y);
        }

        return perimeter;
    }

    // Shoelace formula
    public long CalculateArea(List<Point> vertices)
    {
        int numVertices = vertices.Count;
        long area = 0;

        for (int i = 0; i < numVertices; i++)
        {
            int j = (i + 1) % numVertices;
            long a = (long)vertices[i].X * vertices[j].Y;
            long b = (long)vertices[j].X * vertices[i].Y;
            area += a - b;
        }

        return Math.Abs(area / 2);
    }

}
