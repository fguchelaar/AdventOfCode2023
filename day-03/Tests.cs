using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_03;

[TestClass]
public class Tests
{
    const string example = """
            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(4361, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(467835, puzzle.Part2());
    }
}
