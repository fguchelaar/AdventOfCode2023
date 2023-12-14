using System.Text;

namespace day_12;

public class Puzzle(string input)
{
    private readonly IEnumerable<ConditionRecord> records = input.Trim().Split("\n").Select(line =>
        {
            var parts = line.Split(" ");
            var mask = parts[0];
            var groups = parts[1].Split(",").Select(int.Parse).ToArray();
            return new ConditionRecord(mask, groups);
        });


    public int Part1()
    {
        var sum = 0;
        foreach (var record in records)
        {
            var maskLength = record.Mask.Length;
            var ballsToDivide = maskLength - record.Groups.Sum();
            var groups = record.Groups.Length + 1;
            var combinations = Helper.DivideBalls(ballsToDivide, groups);
            foreach (var combination in combinations)
            {
                var value = new StringBuilder();
                value.Append('.', combination[0]);
                for (int i = 1; i < combination.Count; i++)
                {
                    value.Append('#', record.Groups[i - 1]);
                    value.Append('.', combination[i]);
                }
                if (record.IsValid(value.ToString())) sum++;

            }
        }

        return sum;
    }

    public long Part2()
    {
        var sum = 0L;
        var steps = 0;
        foreach (var record in records)
        {
            var sum1 = NewMethod(record);
            var newCondition = new ConditionRecord(
                (record.Mask.EndsWith('#') ? "" : "?")
                    + record.Mask
                    + "?"
                    + record.Mask,
                [.. record.Groups, .. record.Groups]
                );
            var sum2 = NewMethod(newCondition);

            sum += sum1 * sum2 * sum2;// * sum2 * sum2;
            Console.WriteLine($"{++steps}: {sum}");
        }

        return sum;
    }

    private static int NewMethod(ConditionRecord record)
    {
        int sum = 0;
        var maskLength = record.Mask.Length;
        var ballsToDivide = maskLength - record.Groups.Sum();
        var groups = record.Groups.Length + 1;
        var combinations = Helper.DivideBalls(ballsToDivide, groups);
        foreach (var combination in combinations)
        {
            var value = new StringBuilder();
            value.Append('.', combination[0]);
            for (int i = 1; i < combination.Count; i++)
            {
                value.Append('#', record.Groups[i - 1]);
                value.Append('.', combination[i]);
            }
            if (record.IsValid(value.ToString())) sum++;
        }

        return sum;
    }
}

record ConditionRecord(string Mask, int[] Groups)
{
    public bool IsValid(string value)
    {
        if (value.Length != Mask.Length) return false;
        for (int i = 0; i < Mask.Length; i++)
        {
            if (Mask[i] == '?') continue;
            if (Mask[i] != value[i]) return false;
        }
        return true;
    }
}

static class Helper
{
    public static List<List<int>> DivideBalls(int N, int M)
    {
        var result = new List<List<int>>();
        for (int firstEmpty = 0; firstEmpty <= 1; firstEmpty++)
        {
            for (int lastEmpty = 0; lastEmpty <= 1; lastEmpty++)
            {
                int effectiveM = M - firstEmpty - lastEmpty;
                if (effectiveM <= 0 && N > 0) continue;
                if (N < effectiveM) continue;
                if (effectiveM == 0)
                {
                    result.Add(new List<int>(new int[M]));
                    continue;
                }

                foreach (var combination in DistributeBalls(N, M, effectiveM))
                {
                    var fullCombination = new List<int>();
                    if (firstEmpty == 1) fullCombination.Add(0);
                    fullCombination.AddRange(combination);
                    if (lastEmpty == 1) fullCombination.Add(0);
                    result.Add(fullCombination);
                }
            }
        }
        return result;
    }

    static List<List<int>> DistributeBalls(int N, int M, int effectiveM)
    {
        var result = new List<List<int>>();
        if (effectiveM == 1)
        {
            result.Add(new List<int> { N });
            return result;
        }
        if (N == effectiveM)
        {
            var combination = new List<int>();
            for (int i = 0; i < effectiveM; i++) combination.Add(1);
            result.Add(combination);
            return result;
        }
        for (int i = 1; i <= N - effectiveM + 1; i++)
        {
            foreach (var rest in DistributeBalls(N - i, M, effectiveM - 1))
            {
                var combination = new List<int> { i };
                combination.AddRange(rest);
                result.Add(combination);
            }
        }
        return result;
    }
}
