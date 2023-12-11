using System.Text.RegularExpressions;

namespace day_03;

public partial class Puzzle(string input)
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitsRegex();

    private readonly string[] input = input.Trim().Split("\n");

    public int Part1()
    {
        var result = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var numbers = DigitsRegex().Matches(line);

            foreach (Match number in numbers)
            {
                var leading = Math.Max(0, number.Index - 1);
                var trailing = Math.Min(line.Length - 1, number.Index + number.Length + 1);
                var top = Math.Max(0, i - 1);
                var bottom = Math.Min(input.Length - 1, i + 1);

                for (int r = top; r <= bottom; r++)
                {
                    var mask = input[r].Substring(leading, trailing - leading)!;
                    var isPart = mask.Any(c => !char.IsDigit(c) && c != '.');
                    if (isPart)
                    {
                        result += int.Parse(number.Value);
                        break;
                    }
                }
            }
        }

        return result;
    }

    public int Part2()
    {
        var gears = new Dictionary<(int, int), IList<int>>();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var numbers = DigitsRegex().Matches(line);

            foreach (Match number in numbers)
            {
                var leading = Math.Max(0, number.Index - 1);
                var trailing = Math.Min(line.Length - 1, number.Index + number.Length + 1);
                var top = Math.Max(0, i - 1);
                var bottom = Math.Min(input.Length - 1, i + 1);

                for (int r = top; r <= bottom; r++)
                {
                    var mask = input[r].Substring(leading, trailing - leading)!;
                    // find all positions of '*' in the mask
                    var positions = mask.Select((c, i) => (c, i)).Where(t => t.c == '*').Select(t => t.i).ToList();
                    foreach (var position in positions)
                    {
                        var p = position + leading;
                        if (!gears.ContainsKey((r, p)))
                        {
                            gears[(r, p)] = new List<int>();
                        }

                        gears[(r, p)].Add(int.Parse(number.Value));
                    }
                }
            }
        }
        // sum up all gears with exactly 2 numbers
        return gears
            .Where(g => g.Value.Count == 2)
            .Sum(p => p.Value[0] * p.Value[1]);
    }

}
