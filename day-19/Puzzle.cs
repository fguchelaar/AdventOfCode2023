namespace day_19;

record Part(int X, int M, int A, int S)
{
    public int Rating => X + M + A + S;
}

struct Rule(string input)
{
    public override string ToString() => input;

    public string? PerformOn(Part part)
    {
        var ruleParts = input.Split(":");
        if (ruleParts.Length == 1) { return ruleParts[0]; }

        var property = ruleParts[0][0];
        var oper = ruleParts[0][1];
        var operand = int.Parse(ruleParts[0][2..]);

        var value = property switch
        {
            'x' => part.X,
            'm' => part.M,
            'a' => part.A,
            's' => part.S,
            _ => throw new Exception($"Unknown property {property}")
        };

        return oper switch
        {
            '<' => value < operand ? ruleParts[1] : null,
            '>' => value > operand ? ruleParts[1] : null,
            '=' => value == operand ? ruleParts[1] : null,
            _ => throw new Exception($"Unknown operator {oper}")
        };
    }
}
public class Puzzle
{
    private readonly IEnumerable<Part> parts;
    private readonly Dictionary<string, IEnumerable<Rule>> workflows;

    public Puzzle(string input)
    {
        var c = input.Trim().Split("\n\n");

        workflows = c[0].Split("\n").Aggregate(new Dictionary<string, IEnumerable<Rule>>(), (dict, line) =>
        {
            var parts = line.Split("{}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var name = parts[0];
            var rules = parts[1].Split(",").Select(rule => new Rule(rule));
            dict.Add(name, rules);
            return dict;
        });

        parts = c[1].Split("\n").Select(line =>
        {
            var parts = line.Split("{}xmas=,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(parts[0]);
            var m = int.Parse(parts[1]);
            var a = int.Parse(parts[2]);
            var s = int.Parse(parts[3]);
            return new Part(x, m, a, s);
        });
    }

    public int Part1()
    {
        var valid = new List<Part>();

        foreach (var part in parts)
        {
            var rules = workflows["in"];
            var current = part;
            while (true)
            {
                var next = rules
                    .Select(rule => rule.PerformOn(current))
                    .First(p => p != null);

                if (next == "A")
                {
                    valid.Add(current);
                    break;
                }
                else if (next == "R")
                {
                    break;
                }

                rules = workflows[next!];
            }
        }


        return valid.Sum(p => p.Rating);
    }

    public long Part2()
    {
        return -1;
    }
}
