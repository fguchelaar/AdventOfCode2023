using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_06;

[TestClass]
public class Tests
{
    const string example = """
            Time:      7  15   30
            Distance:  9  40  200
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(288, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(71503, puzzle.Part2());
    }
}
