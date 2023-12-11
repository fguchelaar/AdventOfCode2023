namespace AdventKit;

public record Point(int X, int Y)
{
    public Point Left => new(X - 1, Y);
    public Point Right => new(X + 1, Y);
    public Point Up => new(X, Y - 1);
    public Point Down => new(X, Y + 1);

    public IEnumerable<Point> Adjacent => new[] { Up, Right, Down, Left };
}
