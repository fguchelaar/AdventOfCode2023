using AdventKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_21;

[TestClass]
public class Tests
{
    const string example = """
            ...........
            .....###.#.
            .###.##..#.
            ..#.#...#..
            ....#.#....
            .##..S####.
            .##..#...#.
            .......##..
            .##.#.####.
            .##..##.##.
            ...........
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(16, puzzle.Part1(steps: 6));
    }

    [TestMethod]
    [DataRow(6, 16)]
    [DataRow(10, 50)]
    [DataRow(50, 1594)]
    public void TestPart2(int steps, int expected)
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(expected, puzzle.Part2(steps));
    }

    [TestMethod]
    public void TestNormalize()
    {
        int width = 4;
        int height = 4;

        var p = new Point(0, 0);
        var np = p.Left;

        Assert.AreEqual(new Point(3, 0), Puzzle.Normalize(np, width, height));
    }
}
