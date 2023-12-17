using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_17;

[TestClass]
public class Tests
{
    const string example = """
            2413432311323
            3215453535623
            3255245654254
            3446585845452
            4546657867536
            1438598798454
            4457876987766
            3637877979653
            4654967986887
            4564679986453
            1224686865563
            2546548887735
            4322674655533
            """;

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(102, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(94, puzzle.Part2());
    }
}
