using AdventKit;

namespace day_08;

public class Puzzle
{
    private readonly IEnumerable<char> instructions;
    private readonly IDictionary<string, (string, string)> network;

    public Puzzle(string input)
    {
        var parts = input.Trim().Split("\n\n");
        instructions = parts[0].ToCharArray();
        network = parts[1].Split("\n")
            .Select(x => x.Split(new char[] { ' ', '=', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => (x[1], x[2]));
    }

    public int Part1() => NumberOfSteps("AAA", x => x == "ZZZ");

    public long Part2() => network.Keys.Where(x => x.EndsWith("A"))
            .Select(x => (long)NumberOfSteps(x, y => y.EndsWith("Z"))) // use long to avoid overflow!
            .Aggregate(NumberOperations.LCM);

    public int NumberOfSteps(string start, Func<string, bool> stopCondition)
    {
        var steps = 0;
        var current = start;
        foreach (var instruction in instructions.Cycle())
        {
            steps++;
            current = instruction == 'L'
                ? network[current].Item1
                : network[current].Item2;

            if (stopCondition(current))
            {
                break;
            }
        }
        return steps;
    }
}
