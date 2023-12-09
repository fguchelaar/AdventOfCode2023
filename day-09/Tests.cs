using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_09;

[TestClass]
public class Tests
{
    const string example = """
            0 3 6 9 12 15
            1 3 6 10 15 21
            10 13 16 21 30 45
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(114, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(2, puzzle.Part2());
    }
}
