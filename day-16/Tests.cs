using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_16;

[TestClass]
public class Tests
{
    const string example = """
            .|...\....
            |.-.\.....
            .....|-...
            ........|.
            ..........
            .........\
            ..../.\\..
            .-.-/..|..
            .|....-|.\
            ..//.|....
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(46, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(51, puzzle.Part2());
    }
}
