namespace day_01;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestPart1()
    {
        var puzzle = new Puzzle("""
            1abc2
            pqr3stu8vwx
            a1b2c3d4e5f
            treb7uchet
            """);

        Assert.AreEqual(142, puzzle.Part1());
    }

    [TestMethod]
    public void TestPart2()
    {
        var puzzle = new Puzzle("""
            two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen
            """);

        Assert.AreEqual(281, puzzle.Part2());
    }
}
