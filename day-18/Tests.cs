using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_18;

[TestClass]
public class Tests
{
    const string example1 = """
            R 6 (#70c710)
            D 5 (#0dc571)
            L 2 (#5713f0)
            D 2 (#d2c081)
            R 2 (#59c680)
            D 2 (#411b91)
            L 5 (#8ceee2)
            U 2 (#caa173)
            L 1 (#1b58a2)
            U 2 (#caa171)
            R 2 (#7807d2)
            U 3 (#a77fa3)
            L 2 (#015232)
            U 2 (#7a21e3)
            """;
    const string example2 = """
            R 4 (#70c710)
            D 4 (#0dc571)
            L 4 (#5713f0)
            U 4 (#d2c081)
            """;

    const string example3 = """
            R 6 (#70c710)
            D 2 (#0dc571)
            L 2 (#5713f0)
            D 2 (#0dc571)
            L 4 (#5713f0)
            U 4 (#d2c081)
            """;

    const string example4 = """
            R 2 (#70c710)
            D 2 (#0dc571)
            R 2 (#5713f0)
            D 2 (#0dc571)
            L 2 (#5713f0)
            D 2 (#0dc571)
            L 2 (#5713f0)
            U 2 (#0dc571)
            L 2 (#5713f0)
            U 2 (#0dc571)
            R 2 (#70c710)
            U 2 (#0dc571)
            """;
    [TestMethod]
    [DataRow(example1, 62)]
    [DataRow(example2, 25)]
    [DataRow(example3, 31)]
    [DataRow(example4, 33)]
    public void TestPart1(string input, int expected)
    {
        var puzzle = new Puzzle(input);
        Assert.AreEqual(expected, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example1);
        Assert.AreEqual(952408144115, puzzle.Part2());
    }
}
