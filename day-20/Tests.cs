using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_20;

[TestClass]
public class Tests
{
    const string example1 = """
            broadcaster -> a, b, c
            %a -> b
            %b -> c
            %c -> inv
            &inv -> a
            """;
    const string example2 = """
            broadcaster -> a
            %a -> inv, con
            &inv -> b
            %b -> con
            &con -> output
            """;

    [TestMethod]
    [DataRow(example1, 32000000)]
    [DataRow(example2, 11687500)]
    public void TestPart1(string input, int expected)
    {
        var puzzle = new Puzzle(input);
        Assert.AreEqual(expected, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle(example1);
        Assert.AreEqual(-1, puzzle.Part2());
    }
}
