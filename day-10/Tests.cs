using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_10;

[TestClass]
public class Tests
{
    const string example1 = """
            .....
            .S-7.
            .|.|.
            .L-J.
            .....
            """;

    const string example2 = """
            ..F7.
            .FJ|.
            SJ.L7
            |F--J
            LJ...
            """;

    [TestMethod]
    [DataRow(example1, 4)]
    [DataRow(example2, 8)]
    public void TestPart1(string input, int expected)
    {
        Assert.AreEqual(expected, new Puzzle(input).Part1());
    }

    const string example3 = """
            ...........
            .S-------7.
            .|F-----7|.
            .||.....||.
            .||.....||.
            .|L-7.F-J|.
            .|..|.|..|.
            .L--J.L--J.
            ...........
            """;

    const string example4 = """
            ..........
            .S------7.
            .|F----7|.
            .||....||.
            .||....||.
            .|L-7F-J|.
            .|..||..|.
            .L--JL--J.
            ..........
            """;

    const string example5 = """
            .F----7F7F7F7F-7....
            .|F--7||||||||FJ....
            .||.FJ||||||||L7....
            FJL7L7LJLJ||LJ.L-7..
            L--J.L7...LJS7F-7L7.
            ....F-J..F7FJ|L7L7L7
            ....L7.F7||L7|.L7L7|
            .....|FJLJ|FJ|F7|.LJ
            ....FJL-7.||.||||...
            ....L---J.LJ.LJLJ...
            """;

    const string example6 = """
            FF7FSF7F7F7F7F7F---7
            L|LJ||||||||||||F--J
            FL-7LJLJ||||||LJL-77
            F--JF--7||LJLJ7F7FJ-
            L---JF-JLJ.||-FJLJJ7
            |F|F-JF---7F7-L7L|7|
            |FFJF7L7F-JF7|JL---7
            7-L-JL7||F7|L7F-7F7|
            L.L7LFJ|||||FJL7||LJ
            L7JLJL-JLJLJL--JLJ.L            
            """;

    [TestMethod]
    [DataRow(example3, 4)]
    [DataRow(example4, 4)]
    [DataRow(example5, 8)]
    [DataRow(example6, 10)]
    public void TestPart2(string input, int expected)
    {
        Assert.AreEqual(expected, new Puzzle(input).Part2());
    }
}
