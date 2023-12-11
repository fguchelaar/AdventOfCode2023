using System.Text.RegularExpressions;

namespace day_01;

public partial class Puzzle(string input)
{
    private readonly string[] input = input.Trim().Split("\n");

    public int Part1() => input
            .Select(row => row.First(c => char.IsDigit(c)) + "" + row.Last(c => char.IsDigit(c)))
            .Select(int.Parse)
            .Sum();

    [GeneratedRegex(@"(\d|one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex NumberFindRegex();

    public int Part2() => input
            .Select(row =>
            {
                var matches = NumberFindRegex().Matches(row);
                return Map(matches.First().Value) + Map(matches.Last().Value);
            })
            .Select(int.Parse)
            .Sum();

    string Map(string str) => str switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => str,
    };
}
