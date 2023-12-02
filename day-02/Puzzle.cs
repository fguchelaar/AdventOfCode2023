using System.Text.RegularExpressions;

namespace day_02;

public class Puzzle
{
    private readonly IEnumerable<Game> games;

    public Puzzle(string input)
    {
        games = input.Trim().Split("\n").Select(line => new Game(line));
    }

    public int Part1() => games.Where(game => game.IsPossible).Sum(g => g.Id);

    public int Part2() => games.Sum(g => g.Power);
}

record Game(string info)
{
    public int Id => int.Parse(info[5..info.IndexOf(':')]);

    // only 12 red cubes, 13 green cubes, and 14 blue cubes
    public bool IsPossible =>
        MaxOfColor("red") <= 12
        && MaxOfColor("green") <= 13
        && MaxOfColor("blue") <= 14;

    // equal to the numbers of red, green, and blue cubes multiplied together
    public int Power =>
        MaxOfColor("red") * MaxOfColor("green") * MaxOfColor("blue");

    // the maximum number of cubes of the given color
    private int MaxOfColor(string color) =>
        new Regex($@"(\d+) {color}")
            .Matches(info)
            .Select(m => int.Parse(m.Groups[1].Value))
            .Max();
}
