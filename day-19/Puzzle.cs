namespace day_19;

record Part(int X, int M, int A, int S)
{
    public int Rating => X + M + A + S;
}

record RangedPart(Range X, Range M, Range A, Range S)
{
    public long TotalCombinations =>
         (long)(X.End - X.Start + 1)
        * (M.End - M.Start + 1)
        * (A.End - A.Start + 1)
        * (S.End - S.Start + 1);
}

record Range(int Start, int End)
{
    public Range[] Split(int until)
    {
        if (until < Start || until >= End)
        {
            return [this];
        }
        else
        {
            return [
                new Range(Start, until),
                new Range(until + 1, End)
            ];
        }
    }
}

struct Rule(string input)
{
    public readonly string? PerformOn(Part part)
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
            _ => throw new Exception($"Unknown operator {oper}")
        };
    }

    public readonly (string? next, RangedPart? include, RangedPart? exluded) PerformOn(RangedPart rangedPart)
    {
        var ruleParts = input.Split(":");
        if (ruleParts.Length == 1) { return (ruleParts[0], rangedPart, null); }

        var property = ruleParts[0][0];
        var oper = ruleParts[0][1];
        var operand = int.Parse(ruleParts[0][2..]);

        var value = property switch
        {
            'x' => rangedPart.X,
            'm' => rangedPart.M,
            'a' => rangedPart.A,
            's' => rangedPart.S,
            _ => throw new Exception($"Unknown property {property}")
        };

        var next = oper switch
        {
            '<' => value.Start < operand || value.End < operand ? ruleParts[1] : null,
            '>' => value.Start > operand || value.End > operand ? ruleParts[1] : null,
            _ => throw new Exception($"Unknown operator {oper}")
        };

        if (next == null)
        {
            return (null, null, rangedPart);
        }
        else
        {
            var split = value.Split(oper == '<' ? operand - 1 : operand);
            var (a, b) = oper == '<' ? (split[0], split[1]) : (split[1], split[0]);
            var (include, exclude) = property switch
            {
                'x' => (new RangedPart(a, rangedPart.M, rangedPart.A, rangedPart.S), new RangedPart(b, rangedPart.M, rangedPart.A, rangedPart.S)),
                'm' => (new RangedPart(rangedPart.X, a, rangedPart.A, rangedPart.S), new RangedPart(rangedPart.X, b, rangedPart.A, rangedPart.S)),
                'a' => (new RangedPart(rangedPart.X, rangedPart.M, a, rangedPart.S), new RangedPart(rangedPart.X, rangedPart.M, b, rangedPart.S)),
                's' => (new RangedPart(rangedPart.X, rangedPart.M, rangedPart.A, a), new RangedPart(rangedPart.X, rangedPart.M, rangedPart.A, b)),
                _ => throw new Exception($"Unknown property {property}")
            };
            return (next, include, exclude);
        }
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
        var initial = new RangedPart(
            new Range(1, 4000),
            new Range(1, 4000),
            new Range(1, 4000),
            new Range(1, 4000)
        );

        var accepted = new List<RangedPart>();

        var queue = new Queue<(string workflow, RangedPart rangedPart)>();
        queue.Enqueue(("in", initial));

        while (queue.Count > 0)
        {
            var (workflow, rangedPart) = queue.Dequeue();
            var rules = workflows[workflow];
            foreach (var rule in rules)
            {
                var (next, include, exclude) = rule.PerformOn(rangedPart);
                if (next == "A")
                {
                    accepted.Add(include!);
                }
                else if (next != null && next != "R") // don't process rejected parts any further
                {
                    queue.Enqueue((next, include!));
                }

                if (exclude != null)
                {
                    rangedPart = exclude;
                }
                else
                {
                    break;
                }
            }
        }

        return accepted.Sum(p => p.TotalCombinations);
    }
}
