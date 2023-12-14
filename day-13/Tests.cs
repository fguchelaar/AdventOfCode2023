using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day_13;

[TestClass]
public class Tests
{
    const string example = """
            #.##..##.
            ..#.##.#.
            ##......#
            ##......#
            ..#.##.#.
            ..##..##.
            #.#.##.#.

            #...##..#
            #....#..#
            ..##..###
            #####.##.
            #####.##.
            ..##..###
            #....#..#
            """;

    const string example2 = """
            #.##..##.
            ..#.##.#.
            ##......#
            ##......#
            ..#.##.#.
            ..##..##.
            #.#.##.#.

            #...##..#
            #....#..#
            ..##..###
            #####.##.
            #####.##.
            ..##..###
            #....#..#

            .#.##.#.#
            .##..##..
            .#.##.#..
            #......##
            #......##
            .#.##.#..
            .##..##.#

            #..#....#
            ###..##..
            .##.#####
            .##.#####
            ###..##..
            #..#....#
            #..##...#
            """;
    [TestMethod]
    [DataRow(example, 405)]
    [DataRow(example2, 709)]
    public void TestPart1(string input, int expected)
    {
        var puzzle = new Puzzle(input);
        Assert.AreEqual(expected, puzzle.Part1());
    }

    [TestMethod]
    [DataRow(example, 400)]
    [DataRow(example2, 1400)]
    public void TestPart2(string input, int expected)
    {
        var puzzle = new Puzzle(input);
        Assert.AreEqual(expected, puzzle.Part2());
    }

    [TestMethod]
    public void Test()
    {
        var puzzle = new Puzzle("""
..#..##..##
..#..##..##
##...#.#.#.
#.#.#.#....
.###..####.
##.#.....##
..#.#.##..#
...#...##..
#########.#
#####.###.#
...#...##..
..#.#.##..#
##.#.....##
.###..####.
#.#.#.#....
""");
        Assert.AreEqual(100, puzzle.Part1());
        Assert.AreEqual(900, puzzle.Part2());
    }
}
