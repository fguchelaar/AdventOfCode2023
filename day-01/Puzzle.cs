using System.Text.RegularExpressions;

namespace day_01;

public class Puzzle
{
    private readonly string[] input;

    public Puzzle(string input)
    {
        this.input = input.Trim().Split("\n");
    }

    public int Part1()
    {
        return input
            .Select(row => row.First(c => char.IsDigit(c)) + "" + row.Last(c => char.IsDigit(c)))
            .Select(int.Parse)
            .Sum();
    }

    public int Part2()
    {
        var regex = new Regex(@"(\d|one|two|three|four|five|six|seven|eight|nine)");
        return input
            .Select(row =>
            {
                var matches = regex.Matches(row);
                return Map(matches.First().Value) + Map(matches.Last().Value);
            })
            .Select(int.Parse)
            .Sum();
    }

    string Map(string str)
    {
        switch (str)
        {
            case "one": return "1";
            case "two": return "2";
            case "three": return "3";
            case "four": return "4";
            case "five": return "5";
            case "six": return "6";
            case "seven": return "7";
            case "eight": return "8";
            case "nine": return "9";
            default: return str;
        }
    }
}
