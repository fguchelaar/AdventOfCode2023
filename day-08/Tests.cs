using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_08;

[TestClass]
public class Tests
{
    const string example1 = """
            RL

            AAA = (BBB, CCC)
            BBB = (DDD, EEE)
            CCC = (ZZZ, GGG)
            DDD = (DDD, DDD)
            EEE = (EEE, EEE)
            GGG = (GGG, GGG)
            ZZZ = (ZZZ, ZZZ)
            """;

    const string example2 = """
            LLR

            AAA = (BBB, BBB)
            BBB = (AAA, ZZZ)
            ZZZ = (ZZZ, ZZZ)
            """;

    const string example3 = """
            LR

            11A = (11B, XXX)
            11B = (XXX, 11Z)
            11Z = (11B, XXX)
            22A = (22B, XXX)
            22B = (22C, 22C)
            22C = (22Z, 22Z)
            22Z = (22B, 22B)
            XXX = (XXX, XXX)
            """;

    [TestMethod]
    public void TestPart1()
    {
        Assert.AreEqual(2, new Puzzle(example1).Part1());
        Assert.AreEqual(6, new Puzzle(example2).Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        Assert.AreEqual(6, new Puzzle(example3).Part2());
    }
}
