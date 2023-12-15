using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_15;

[TestClass]
public class Tests
{
    const string example = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(1320, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(145, puzzle.Part2());
    }
}
