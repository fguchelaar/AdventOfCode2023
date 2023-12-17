namespace AdventKit;

public record Point(int X, int Y)
{
    public static Point Zero => new(0, 0);

    public Point Left => new(X - 1, Y);
    public Point Right => new(X + 1, Y);
    public Point Up => new(X, Y - 1);
    public Point Down => new(X, Y + 1);

    public IEnumerable<Point> Adjacent => new[] { Up, Right, Down, Left };

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public int Manhattan(Point other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public override string ToString() => $"({X}, {Y})";

}
