using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_12;

[TestClass]
public class Tests
{
    const string example = """
            ???.### 1,1,3
            .??..??...?##. 1,1,3
            ?#?#?#?#?#?#?#? 1,3,1,6
            ????.#...#... 4,1,1
            ????.######..#####. 1,6,5
            ?###???????? 3,2,1
            """;

    [TestMethod]
    [DataRow("???", "###", true)]
    [DataRow("?.?", "###", false)]
    [DataRow("?.?.##", "..#.##", true)]
    public void TestMask(string mask, string value, bool expected)
    {
        var record = new ConditionRecord(mask, []);
        Assert.AreEqual(expected, record.IsValid(value));
    }

    [TestMethod]
    [DataRow("???.### 1,1,3", 1)]
    [DataRow(".??..??...?##. 1,1,3", 16384)]
    [DataRow("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [DataRow("????.#...#... 4,1,1", 16)]
    [DataRow("????.######..#####. 1,6,5", 2500)]
    [DataRow("?###???????? 3,2,1", 506250)]
    public void DoIt(string record, int expected)
    {
        var puzzle = new Puzzle(record);
        Assert.AreEqual(expected, puzzle.Part2());
    }

    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle(example);
        Assert.AreEqual(21, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        Assert.Inconclusive();
        var puzzle = new Puzzle(example);
        Assert.AreEqual(525152, puzzle.Part2());
    }
}
