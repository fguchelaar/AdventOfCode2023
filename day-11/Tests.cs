using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_11;

[TestClass]
public class Tests
{
    const string example = """
            ...#......
            .......#..
            #.........
            ..........
            ......#...
            .#........
            .........#
            ..........
            .......#..
            #...#.....
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(374, puzzle.Part1());
    }

    [TestMethod]
    [DataRow(10, 1030)]
    [DataRow(100, 8410)]
    public void TestSolve(int expandBy, int expected)
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(expected, puzzle.Solve(expandBy));
    }
}
